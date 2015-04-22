using UnityEngine;
using System.Collections;

/// <summary>
/// Handles the following Module events:
/// -- calcualtes damage to module in-game
/// 	OnTriggerEnter(Collider col);
/// 	ApplyPlasmaDamage(Collider col);
/// 	CalcualtePureDamage(int damage);
/// 	DestroyThisModule();
/// -- removes the module stats from the contraption
/// 	RemoveStats();
/// -- when the user touches on the object in the Constructor Menu
/// 	OnDrag(); 
/// 	OnScroll();
/// 	OnDoubleClick();
/// 	OnClick();
/// -- called by pressing the rotate button
/// 	Rotate();
/// </summary>

public class Chosen : MonoBehaviour
{
	public int ID;
	public bool disabled;

	private Graph G;
	private MouseOrbitImproved cameraScript;

	void Start ()
	{
		//Add the stats of this new module to the Player Stats 
		//and then spread the new Player stats to the children

		if (transform.parent != null) {  //BUG - parent throws null when switching scenes...
			transform.parent.GetComponent<Stats> ().AddToStats (GetComponent<ModuleStats> ());
			G = transform.parent.GetComponent<Graph> ();

			//Add the module to the graph
			ID = G.Count ();
			G.AddVertex (ID, transform.position);
		}

		//Tells the rotate script to rotate around the MAIN CAMERA
		cameraScript = Camera.main.GetComponent<MouseOrbitImproved> ();
		OnClick ();
	}

	//Collision detection
	void OnTriggerEnter (Collider col)
	{
		if (col.gameObject.name == "Plasma(Clone)") {
			//if the object has a parent(not already dead) apply damage
			if (transform.parent != null && !disabled) 
				ApplyPlasmaDamage (col);
			Destroy (col.gameObject);  //Dispose of the colliding Plasma object
		}
	}

	//Calcualte precisely how much to reduce the HP and whether to kill the block or not
	void ApplyPlasmaDamage (Collider col)
	{
		GetComponent<ModuleStats> ().HP -= CalculatePureDamage (col.gameObject.GetComponent<Plasma> ().damage);
		if (GetComponent<ModuleStats> ().HP < 1 && GetComponent<Rigidbody> () == null) {
			if (G.ArticulationPoints.Contains (ID)) {  //If it's an articulation point
				disabled = true;
				SwitchOffThisModule();  //Remove stats and turn lights off
				//TODO: CHANGE APPEARENCE 
			} else {
				DestroyThisModule ();
			}
		}
	}

	float CalculatePureDamage (int damage)
	{
		float dam = damage * (100F / (100F + GetComponent<ModuleStats> ().protection));
		return dam;
	}

	//Safely destroy this module, by removing its stats and Sphere Collider from the parent Player,
	// disabling its light, making it parentless, adding a rigidbody for collision,
	// removing 1 from the Player mass and making its collider non-trigger
	public void DestroyThisModule ()
	{
		//TODO: Spring cube out
		Vector3 spherePos = transform.localPosition;
		foreach (SphereCollider sphereCol in transform.parent.GetComponents<SphereCollider>()) {
			if (sphereCol.center == spherePos)
				Destroy (sphereCol);
		}

		SwitchOffThisModule ();
		transform.parent.GetComponent<Rigidbody> ().mass--;
		transform.parent = null;
		gameObject.AddComponent<Rigidbody> ().drag = 1;
		gameObject.GetComponent<SphereCollider> ().isTrigger = false;

		//Remove it from the Graph and find other connected and disabled modules
		G.RemoveVertex (ID);
		foreach (Transform module in GameObject.Find("Player").GetComponentInChildren<Transform>()) {
			Chosen ch = module.GetComponent<Chosen> ();
			if (ch.disabled && !G.ArticulationPoints.Contains (ch.ID)) {
				ch.DestroyThisModule ();
			}
		}
	}

	//Removes the effects this module brings to stats (and turns the light off)
	void SwitchOffThisModule ()
	{
		//Remove bonus from statistics
		transform.parent.GetComponent<Stats> ().RemoveFromStats (gameObject.GetComponent<ModuleStats> ());
		//Remove visuals
		//gameObject.GetComponent<Light> ().enabled = false;
	}

	void OnDrag ()
	{
		if (cameraScript == null)
			cameraScript = Camera.main.GetComponent<MouseOrbitImproved> ();
		cameraScript.OnDrag ();
	}

	void OnScroll ()
	{
		if (cameraScript == null)
			cameraScript = Camera.main.GetComponent<MouseOrbitImproved> ();
		cameraScript.OnScroll ();
	}

	//Actually destroys the module on double click or X button
	//Also moves the frame around it to the Main Module
	public void OnDoubleClick ()
	{
		//If this module is not an articulation point (cut-vertex) to the robot
		if (!G.ArticulationPoints.Contains (ID)) {
			GameObject.Find ("Main Module").GetComponent<Chosen> ().OnClick ();
			if (gameObject.name != "Main Module") {
				SwitchOffThisModule();
				G.RemoveVertex (ID);
				Destroy (gameObject);
			}
		}
	}
	
	public void OnClick ()
	{
		//Basically checks if we are on the first loadlevel (main scene)...
		if (cameraScript != null) {
			//...then set the camera rotation target and the frame position to the clicked module
			cameraScript.changeTarget (transform);
			GameObject.FindGameObjectWithTag ("Cube Frame").transform.position = transform.position;
			GameObject.FindGameObjectWithTag ("Cube Frame").transform.parent = transform;
			//Displays the seperate module stats
			GameObject.Find ("Module Stats").GetComponent<ShowModuleStats> ().
				UpdateStats (transform.GetComponent<ModuleStats> ());
		}
	}

	//The rotate button
	public void Rotate ()
	{
		transform.rotation = Quaternion.Euler (0, transform.eulerAngles.y + 90, 0);
	}
}

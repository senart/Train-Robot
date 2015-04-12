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
	MouseOrbitImproved cameraScript;

	void Awake ()
	{
		cameraScript = Camera.main.GetComponent<MouseOrbitImproved> ();
		OnClick ();  //Simulate a click on this module
	}

	//Collision detection
	void OnTriggerEnter (Collider col)
	{
		if (col.gameObject.name == "Plasma(Clone)") ApplyPlasmaDamage (col);
	}

	//Calcualte precisely how much to reduce the HP and whether to kill the block or not
	void ApplyPlasmaDamage (Collider col)
	{
		//if the object has a parent(not already dead) then apply damage
		if (transform.parent != null) {
			GetComponent<ModuleStats> ().HP -= CalculatePureDamage(col.gameObject.GetComponent<Plasma> ().damage);
			if (GetComponent<ModuleStats> ().HP < 1 && GetComponent<Rigidbody> () == null) {
				DestroyThisModule ();
			}
		}
		Destroy (col.gameObject);  //Dispose of the colliding object
	}

	float CalculatePureDamage(int damage)
	{
		float dam = damage * (100F / (100F + GetComponent<ModuleStats> ().protection));
		return dam;
	}

	//Safely destroy this module, by removing its stats and Sphere Collider from the parent Player,
	// disabling its light, making it parentless, adding a rigidbody for collision,
	// removing 1 from the Player mass and making its collider non-trigger
	void DestroyThisModule ()
	{
		//TODO: Spring cube out
		Vector3 spherePos = transform.localPosition;
		foreach (SphereCollider sphereCol in transform.parent.GetComponents<SphereCollider>()) {
			if (sphereCol.center == spherePos)
				Destroy (sphereCol);
		}

		RemoveStats ();
		gameObject.GetComponent<Light> ().enabled = false;
		transform.parent.GetComponent<Rigidbody> ().mass--;
		transform.parent = null;
		gameObject.AddComponent<Rigidbody> ().drag = 1;
		gameObject.GetComponent<SphereCollider> ().isTrigger = false;
	}

	//Removes the effects this module brings to the all-stats
	void RemoveStats()
	{
		transform.parent.GetComponent<Stats>().RemoveFromStats(gameObject.GetComponent<ModuleStats>());
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
		GameObject.Find ("Main Module").GetComponent<Chosen> ().OnClick ();
		if (gameObject.name != "Main Module") {
			RemoveStats();
			Destroy (gameObject);
		}
	}
	
	public void OnClick ()
	{
		//Basically checks if we are on the first loadlevel (main scene)...
		if (cameraScript != null) {
			cameraScript.changeTarget (transform);
			GameObject.FindGameObjectWithTag ("Cube Frame").transform.position = transform.position;
			GameObject.FindGameObjectWithTag ("Cube Frame").transform.parent = transform;
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

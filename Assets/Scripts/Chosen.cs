using UnityEngine;
using System.Collections;

/// <summary>
/// Handles the following Module events:
/// -- calcualtes damage to module in-game
/// 	OnTriggerEnter(Collider col);
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

	public float health = 5.0F;

	void OnLevelWasLoaded (int level)
	{
		//When the Build Scene is loaded
		if (level == 0) {
			cameraScript = Camera.main.GetComponent<MouseOrbitImproved> ();
			OnClick ();  //Simulate a click on this module
		}
	}

	//Collision detection
	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.name == "Plasma(Clone)") {
			if(health == 1 && GetComponent<Rigidbody>() == null) 
			{
				DestroyThisModule();
			}
			else health--;
			Destroy (col.gameObject);
		}
	}

	//Safely destroy this module, by removing its Sphere Collider from the parent Player,
	// disabling its light, making it parentless, adding a rigidbody for collision
	// and making it a non-trigger collider
	void DestroyThisModule()
	{
		//TODO: Spring cube out
		Vector3 spherePos = transform.localPosition;
		foreach (SphereCollider sphereCol in GameObject.Find ("Player").GetComponents<SphereCollider>()){
			if(sphereCol.center == spherePos)
				Destroy(sphereCol);
		}

		gameObject.GetComponent<Light> ().enabled = false;
		gameObject.gameObject.transform.parent = null;
		gameObject.AddComponent<Rigidbody> ().drag = 1;
		gameObject.GetComponent<SphereCollider>().isTrigger = false;
	}

	void OnDrag ()
	{
		if (cameraScript == null) cameraScript = Camera.main.GetComponent<MouseOrbitImproved> ();
		cameraScript.OnDrag ();
	}

	void OnScroll ()
	{
		if (cameraScript == null) cameraScript = Camera.main.GetComponent<MouseOrbitImproved> ();
		cameraScript.OnScroll ();
	}

	//Actually destroys the module on double click or X button
	//Also moves the frame around it to the Main Module
	public void OnDoubleClick ()
	{
		GameObject.Find("Main Module").GetComponent<Chosen>().OnClick();
		if(gameObject.name != "Main Module")
			Destroy (gameObject);
	}
	
	public void OnClick ()
	{
		//Because DragItem.cs calls this before Start is executed
		if (cameraScript == null)
			cameraScript = Camera.main.GetComponent<MouseOrbitImproved> ();

		//Use Lerp on the following two transform changes
		cameraScript.changeTarget (transform);
		GameObject.FindGameObjectWithTag ("Cube Frame").transform.position = transform.position;
		GameObject.FindGameObjectWithTag ("Cube Frame").transform.parent = transform;
	}

	//The rotate button
	public void Rotate()
	{
		transform.rotation = Quaternion.Euler (0, transform.eulerAngles.y + 90, 0);
	}
}

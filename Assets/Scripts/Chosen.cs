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
		if (level == 0) {
			cameraScript = Camera.main.GetComponent<MouseOrbitImproved> ();
			OnClick ();
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.name == "Plasma(Clone)") {
			if(health == 1 && GetComponent<Rigidbody>() == null) 
			{
				DestroyThisCube();
			}
			else health--;
			Destroy (col.gameObject);
		}
	}

	void DestroyThisCube()
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

	public void Rotate()
	{
		transform.rotation = Quaternion.Euler (0, transform.eulerAngles.y + 90, 0);
	}
}

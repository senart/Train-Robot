using UnityEngine;
using System.Collections;

public class RotateItem : MonoBehaviour {
	
	// Update is called once per frame
	void OnClick () {
		GameObject.FindGameObjectWithTag ("Cube Frame").transform.parent.GetComponent<Chosen> ().Rotate ();
	}
}

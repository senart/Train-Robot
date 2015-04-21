using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

	public Vector3 rotation;

	// Update is called once per frame
	void Update () {
		gameObject.transform.Rotate(rotation*Time.deltaTime);
	}
}

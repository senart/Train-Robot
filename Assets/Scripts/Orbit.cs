using UnityEngine;
using System.Collections;

public class Orbit : MonoBehaviour {
	
	public Vector3 v = Vector3.zero;
	public Vector3 axis = Vector3.up;
	public float orbitSpeed = 180.0f;
	public Transform ship;

	void Start()
	{
		v = GetComponent<Rigidbody> ().position;
	}

	void FixedUpdate () {
		Quaternion q = Quaternion.AngleAxis (orbitSpeed * Time.fixedDeltaTime, axis);
		v = q * v;
		GetComponent<Rigidbody> ().MovePosition (ship.position + v);
		GetComponent<Rigidbody> ().MoveRotation (q * transform.rotation);
	}
}
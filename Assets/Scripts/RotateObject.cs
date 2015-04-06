using UnityEngine;
using System.Collections;
/// <summary>
/// MAY BE USED LATER, ROTATES OBJECT AROUND OTHER OBJECT
/// </summary>
public class RotateObject : MonoBehaviour {
	
	public float rotationSpeed = 200;
	public GameObject joystickRotation;
	public Vector3 axis = Vector3.up;
	private UIJoyStick joyRotate;

	Vector3 v;

	void OnLevelWasLoaded (int level)
	{
		if (level == 1) {
			v = GetComponent<Rigidbody>().position;
			joystickRotation = GameObject.Find ("Rotation");
			if (joystickRotation != null) {
				joyRotate = joystickRotation.GetComponent<UIJoyStick> ();
			}
			
		}
	}

	// Rotates around Main Module
	void FixedUpdate () {
		if (joyRotate != null) {
			//Rotate around the Main Module
			Quaternion q = Quaternion.AngleAxis (rotationSpeed*joyRotate.joyStickPosX * Time.fixedDeltaTime, axis);
			v = q * v;
			GetComponent<Rigidbody>().MovePosition (GameObject.Find ("Main Module").transform.position + v);
			GetComponent<Rigidbody> ().MoveRotation (q * transform.rotation);
		}
	}
}

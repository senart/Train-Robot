using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundry
{
	public float xMin, xMax, zMin, zMax;
}

public class Move : MonoBehaviour
{
	public Boundry boundry;
	public float speed = 10;
	public float tilt = 10;
	public float tiltLerpSpeed = 5;
	public float deadZone = 0.0001F;
	public float maxVelocity = 10;

	public GameObject joystickMovement;
	public GameObject joystickRotation;

	private float angle = 0;
	private UIJoyStick joyMove;
	private UIJoyStick joyRotate;
	private Vector3 movement;
	private Vector3 velocity;
	private Rigidbody rigid;
	private float sqrMaxVelocity;

	void OnLevelWasLoaded (int level)
	{
		//When the Arena Scene loads
		if (level == 1) {
			sqrMaxVelocity = maxVelocity * maxVelocity;
			rigid = GetComponent<Rigidbody> ();
			rigid.isKinematic = false;
			joystickMovement = GameObject.Find ("Movement");
			joystickRotation = GameObject.Find ("Rotation");

			if (joystickMovement != null && joystickRotation != null) {
				joyMove = joystickMovement.GetComponent<UIJoyStick> ();
				joyRotate = joystickRotation.GetComponent<UIJoyStick> ();
			}
		}
	}

	void FixedUpdate ()
	{
		//If we can assign the joysticks correctly
		if (joyMove != null && joyRotate != null) {

			// Apply movement from move joystick
			movement = new Vector3 (joyMove.joyStickPosX, 0.0F, joyMove.joyStickPosY);

			//Keyboard movement. Uncomment this and comment out the one below for keyboard testing
			//movement = new Vector3 (Input.GetAxis("Horizontal"), 0.0F, Input.GetAxis("Vertical"));
			rigid.velocity = movement * speed;

			//No idea what this does here...
			if (rigid.velocity.sqrMagnitude > sqrMaxVelocity) {
				rigid.velocity = rigid.velocity.normalized * maxVelocity;
			}

			//Boundries of the playfield
			rigid.position = new Vector3
							(
								Mathf.Clamp (rigid.position.x, boundry.xMin, boundry.xMax),
								0.0F,
								Mathf.Clamp (rigid.position.z, boundry.zMin, boundry.zMax)
			);

			//Dead zone callibration, in order to not have 0 rotation
			if ((Mathf.Abs (joyRotate.joyStickPosX) > deadZone && Mathf.Abs (joyRotate.joyStickPosY) > deadZone)) {
				angle = Mathf.Atan2 (-joyRotate.joyStickPosX, -joyRotate.joyStickPosY) * Mathf.Rad2Deg;
			}

			//Actual rotation
			Quaternion rot = Quaternion.Euler (0, angle, 0);
			Vector3 tiltAxis = Vector3.Cross (Vector3.up, rigid.velocity);
			rot = Quaternion.AngleAxis (tilt, tiltAxis) * rot;
			rigid.rotation = Quaternion.Lerp (rigid.rotation, rot, Time.deltaTime * tiltLerpSpeed);

			//Works but uses momentum
			//GetComponent<Rigidbody> ().AddTorque(transform.up*joyRotate.joyStickPosX*speed,ForceMode.VelocityChange);
		}
	}
}

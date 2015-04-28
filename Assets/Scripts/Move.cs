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
	//Variables for UI controls
	public GameObject joystickMovement;
	public GameObject joystickRotation;
	public float deadZone = 0.0001F;
	private UIJoyStick joyMove;
	private UIJoyStick joyRotate;
	private float angle;

	//Movement variables
	public float MoveSpeed = 15F;
	public float RotateSpeed = 3F;
	Vector3 m_CurrentMovement;
	Vector3 s_CurrentMovement;
	float m_CurrentTurnSpeed;
	Rigidbody rigid;

	//Network variables
	PhotonView m_PhotonView;
	PhotonRigidbodyView m_RigidView;
	public bool isControllable;

	void Start ()
	{
		isControllable = true;
		m_PhotonView = GetComponent<PhotonView> ();
		m_RigidView = GetComponent<PhotonRigidbodyView> ();


		rigid = GetComponent<Rigidbody> ();
		joystickMovement = GameObject.Find ("Movement");
		joystickRotation = GameObject.Find ("Rotation");

		if (joystickMovement != null && joystickRotation != null) {
			joyMove = joystickMovement.GetComponent<UIJoyStick> ();
			joyRotate = joystickRotation.GetComponent<UIJoyStick> ();
		}

		Destroy (GameObject.Find ("TESTER"));
	}

	void Update ()
	{
		if (isControllable) {
			if (m_PhotonView.isMine == true) {
				ResetSpeedValues ();

				if (joyMove != null && joyRotate != null) {
					UpdateForwardMovement ();
					UpdateLeftRighteMovement ();
					UpdateRotateMovement ();
					MoveCharacterController ();
				}
			}
		}
	}
			
	void ResetSpeedValues ()
	{
		m_CurrentMovement = Vector3.zero;
		s_CurrentMovement = Vector3.zero;
		m_CurrentTurnSpeed = 0;
	}
			
	void MoveCharacterController ()
	{
		rigid.AddForce (m_CurrentMovement);
		rigid.AddForce (s_CurrentMovement);
	}
			
	void UpdateForwardMovement ()
	{
		m_CurrentMovement = Vector3.forward * MoveSpeed * joyMove.joyStickPosY;
	}
			
	void UpdateLeftRighteMovement ()
	{
		s_CurrentMovement = Vector3.right * MoveSpeed * joyMove.joyStickPosX;
	}
			
	void UpdateRotateMovement ()
	{
		//JOYSTICK ROTATION
		if ((Mathf.Abs (joyRotate.joyStickPosX) > deadZone && Mathf.Abs (joyRotate.joyStickPosY) > deadZone)) {
			angle = Mathf.Atan2 (-joyRotate.joyStickPosX, -joyRotate.joyStickPosY) * Mathf.Rad2Deg;
		}
		//Actual rotation
		Quaternion rot = Quaternion.Euler (0, angle, 0);
		rigid.rotation = Quaternion.Lerp (rigid.rotation, rot, Time.deltaTime * RotateSpeed);

		//SIMPLE ROTATION
		//rigid.AddTorque(transform.up*RotateSpeed*joyRotate.joyStickPosY);
		//rigid.AddTorque(transform.up*RotateSpeed*joyRotate.joyStickPosX);
	}

//		//For Augmented Reality
//		if (level == 4) {
//			transform.parent = GameObject.Find("ImageTarget").transform;
//			transform.localScale = new Vector3(1,1,1);
//		}

	//OLD MOVEMENT METHOD
//	void FixedUpdate ()
//	{
//		//If we can assign the joysticks correctly
//		if (joyMove != null && joyRotate != null) {
//			
//			// Apply movement from move joystick
//			movement = new Vector3 (joyMove.joyStickPosX, 0.0F, joyMove.joyStickPosY);
//			rigid.velocity = movement * speed;
//			
//			//No idea what this does here...
//			if (rigid.velocity.sqrMagnitude > sqrMaxVelocity) {
//				rigid.velocity = rigid.velocity.normalized * maxVelocity;
//			}
//			
//			//Boundries of the playfield
//			rigid.position = new Vector3
//				(
//					Mathf.Clamp (rigid.position.x, boundry.xMin, boundry.xMax),
//					0.0F,
//					Mathf.Clamp (rigid.position.z, boundry.zMin, boundry.zMax)
//					);
//			
//			//Dead zone callibration, in order to not have 0 rotation
//			if ((Mathf.Abs (joyRotate.joyStickPosX) > deadZone && Mathf.Abs (joyRotate.joyStickPosY) > deadZone)) {
//				angle = Mathf.Atan2 (-joyRotate.joyStickPosX, -joyRotate.joyStickPosY) * Mathf.Rad2Deg;
//			}
//			
//			//Actual rotation
//			Quaternion rot = Quaternion.Euler (0, angle, 0);
//			Vector3 tiltAxis = Vector3.Cross (Vector3.up, rigid.velocity);
//			rot = Quaternion.AngleAxis (tilt, tiltAxis) * rot;
//			rigid.rotation = Quaternion.Lerp (rigid.rotation, rot, Time.deltaTime * tiltLerpSpeed);
//		}
//	}
}

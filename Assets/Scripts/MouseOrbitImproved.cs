using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera-Control/Mouse Orbit with zoom")]
public class MouseOrbitImproved : MonoBehaviour
{

	public Transform targetTransform;
	public float zoomSpeed = 0.1F;
	public float distance = -10.0f;
	public float yMinLimit = -20f;
	public float yMaxLimit = 80f;
	public float distanceMin = .5f;
	public float distanceMax = 20f;
	public float lerpSpeed = 2.5F;
	public Vector3 startingRotation;
	Vector3 offset;
	float x = 0.0f;
	float y = 0.0f;
	Quaternion rotation;
	Vector3 position;
	bool isAnimating = true;
	public float xSpeed = Screen.width / 8;
	public float ySpeed = Screen.height / 8;

	// Use this for initialization
	void Start ()
	{
		offset = targetTransform.position;
		//rotation = targetTransform.rotation;
		position = targetTransform.position;
		rotation = transform.rotation;
		x = transform.rotation.eulerAngles.x;
		y = transform.rotation.eulerAngles.y;
		transform.rotation = targetTransform.rotation;
		StartCoroutine(RotationLerp());

		// Make the rigid body not change rotation
		if (GetComponent<Rigidbody> ())
			GetComponent<Rigidbody> ().freezeRotation = true;
	}

	void Update ()
	{
		offset = Vector3.Lerp (offset, targetTransform.position, Time.deltaTime * lerpSpeed);
		if (!isAnimating) updateCamera ();
	}
	
	public void OnScroll ()
	{
		distance = Mathf.Clamp (distance - Input.GetAxis ("Mouse ScrollWheel") * 5, distanceMin, distanceMax);
		RaycastHit hit;
		if (Physics.Linecast (offset, offset, out hit)) {
			distance -= hit.distance;
		}
	}

	void TouchZoom ()
	{
		// Store both touches.
		Touch touchZero = Input.GetTouch (0);
		Touch touchOne = Input.GetTouch (1);
			
		// Find the position in the previous frame of each touch.
		Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
		Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
			
		// Find the magnitude of the vector (the distance) between the touches in each frame.
		float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
		float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;
			
		// Find the difference in the distances between each frame.
		float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

		distance = Mathf.Clamp (distance + deltaMagnitudeDiff * zoomSpeed, distanceMin, distanceMax);
		RaycastHit hit;
		if (Physics.Linecast (offset, offset, out hit)) {
			distance -= hit.distance;
		}
	}

	public void OnDrag ()
	{
		x -= Input.GetAxis ("Mouse Y") * xSpeed * distance * 0.02f;
		y += Input.GetAxis ("Mouse X") * ySpeed * 0.02f;
		x = ClampAngle (x, yMinLimit, yMaxLimit);

		rotation = Quaternion.Euler (x, y, 0);
	}

	public void changeTarget (Transform newTargetTransform)
	{
		targetTransform = newTargetTransform;
	}

	void updateCamera ()
	{
		if (Input.touchCount == 2)
			TouchZoom ();
		Vector3 negDistance = new Vector3 (0.0f, 0.0f, -distance);
		position = rotation * negDistance + offset;

		transform.rotation = rotation;
		transform.position = position;
	}

	public static float ClampAngle (float angle, float min, float max)
	{
		if (angle < -360F)
			angle += 360F;
		if (angle > 360F)
			angle -= 360F;
		return Mathf.Clamp (angle, min, max);
	}
	
	IEnumerator RotationLerp() {
		isAnimating = true;
		while (Quaternion.Angle(transform.rotation, rotation)>0.2f){
			transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.2f);
			Vector3 negDistance = new Vector3 (0.0f, 0.0f, -distance);
			position = rotation * negDistance + offset;
			transform.position = position;
			yield return null;
		}
		isAnimating = false;
	}
}
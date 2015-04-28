using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Transform targetMove;           // The position that that camera will be following.
	public float smoothing = 5f;        // The speed with which the camera will be following.

	public Vector3 offset;                     // The initial offset from the target.

	void Start()
	{
		if (!targetMove)
			Debug.Log ("The camera couldn't find transform to move about");
		else offset = transform.position - targetMove.position;
	}

	void FixedUpdate ()
	{
		if (targetMove) {
			// Create a postion the camera is aiming for based on the offset from the target.
			Vector3 targetCamPos = targetMove.position + offset;
		
			// Smoothly interpolate between the camera's current position and it's target position.
			transform.position = Vector3.Lerp (transform.position, targetCamPos, smoothing * Time.deltaTime);
		}
	}
}

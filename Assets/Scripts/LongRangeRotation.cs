using UnityEngine;
using System.Collections;

public class LongRangeRotation : MonoBehaviour {

	public Vector3 backSpeed, frontSpeed, backRSpeed, frontRSpeed;
	public Transform back, front, backR, frontR;
	public KeyCode key;
	public bool canShoot;
	float multiplier = 0;
	bool on;


	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(key)) {
			on = true;
		}
		if (Input.GetKeyUp(key)) {
			on = false;
		}
		float m = multiplier * Time.deltaTime;
		back.Rotate(backSpeed*m);
		front.Rotate(frontSpeed*m);
		backR.Rotate(backRSpeed*m);
		frontR.Rotate(frontRSpeed*m);
	}

	void FixedUpdate() {
		if (on&&canShoot){
			multiplier = Mathf.Lerp(multiplier, 10f, 0.04f);
		}else {
			multiplier = Mathf.Lerp(multiplier, 0, 0.05f);
		}
	}

}

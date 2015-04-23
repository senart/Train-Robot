using UnityEngine;
using System.Collections;

public class MoveUpDown : MonoBehaviour {

	public float speed;
	public Vector3 offset, originalPos;

	void Start() {
		originalPos = gameObject.transform.position;
	}
	// Update is called once per frame
	void Update () {
		gameObject.transform.position = originalPos + offset*Mathf.Sin(speed*Time.time);
	}
}

using UnityEngine;
using System.Collections;

public class BackToTrain : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) { Application.LoadLevel("Train"); }
	}
}

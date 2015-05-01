using UnityEngine;
using System.Collections;

public class BackToBuild : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) { Application.LoadLevel("build");}
	}
}

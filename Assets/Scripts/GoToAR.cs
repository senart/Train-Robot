using UnityEngine;
using System.Collections;

public class GoToAR : MonoBehaviour {

	void OnClick ()
	{
		GameObject player = GameObject.Find ("Player");

		player.transform.position = new Vector3 (0, 0, 0);
		DontDestroyOnLoad (player);  //keep contraption

		Destroy(GameObject.FindGameObjectWithTag ("Cube Frame"));  //destroy frame
		
		Application.LoadLevel ("AR");  //Switch scenes
	}
}

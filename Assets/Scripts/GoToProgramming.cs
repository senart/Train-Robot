using UnityEngine;
using System.Collections;

public class GoToProgramming : MonoBehaviour {

	void OnClick ()
	{
		GameObject player = GameObject.Find ("Player");
		
		player.transform.position = new Vector3 (0, 0, 0);
		GameObject brainzObject = new GameObject("Brainz");
		brainzObject.transform.parent = player.transform;

		DontDestroyOnLoad (player);  //keep contraption
		
		Destroy(GameObject.FindGameObjectWithTag ("Cube Frame"));  //destroy frame	
		
		Application.LoadLevel ("Train");  //Switch scenes
	}
}

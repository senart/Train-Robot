using UnityEngine;
using System.Collections;

public class GoToArena : MonoBehaviour {

	public void GoToLocalArena ()
	{
		GameObject player = GameObject.Find("Player");
		DontDestroyOnLoad (player);  //keep contraption

		Application.LoadLevel ("control");  //Switch scenes
	}

	public void GoToNetworkArena ()
	{
		DontDestroyOnLoad (GameObject.Find ("Player"));
		Application.LoadLevel ("control");  //Switch scenes
	}
}

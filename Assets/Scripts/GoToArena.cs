using UnityEngine;
using System.Collections;

public class GoToLocalArena : MonoBehaviour {

	void OnClick ()
	{
		GameObject player = GameObject.Find("Player");
		DontDestroyOnLoad (player);  //keep contraption

		Application.LoadLevel ("control");  //Switch scenes
	}
}

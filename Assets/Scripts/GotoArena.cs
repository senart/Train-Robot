using UnityEngine;
using System.Collections;

public class GotoArena : MonoBehaviour {

	void OnClick ()
	{
		Destroy(GameObject.Find("Cube Frame"));
		DontDestroyOnLoad (GameObject.Find("Player"));
		Application.LoadLevel ("control");  //Switch scenes
	}
}

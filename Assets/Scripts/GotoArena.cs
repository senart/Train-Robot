using UnityEngine;
using System.Collections;

public class GotoArena : MonoBehaviour {

	public GameObject prefab;

	void OnClick ()
	{
		DontDestroyOnLoad (GameObject.Find("Player"));
		Application.LoadLevel ("NetworkScene");  //Switch scenes
	}
}

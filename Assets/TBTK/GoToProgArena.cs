using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GoToProgArena : MonoBehaviour {

	private List<Command> coms;
	public GameObject player;

	public void GoToProg()
	{
		player = GameObject.Find ("Player");
		coms = new List<Command> ();
		foreach (Transform child in GameObject.Find ("Container").GetComponentInChildren<Transform>()) {
			if(child.GetComponent<Command>()){
				child.GetComponent<Command>().UpdateCommands();
				foreach(Transform trans in child.GetComponentsInChildren<Transform>()){
					if(trans.name=="Label") Destroy (trans.gameObject);
					else if(trans.GetComponent<UISprite>()) Destroy(trans.GetComponent<UISprite>());
				}
				child.transform.parent = GameObject.Find("Brainz").transform;
				coms.Add(child.GetComponent<Command>());
			}
		}

		player.transform.position = new Vector3 (0, 0, 0);
		player.AddComponent<RobotBrain>().SetCommands(coms);
		DontDestroyOnLoad (player);
		Application.LoadLevel ("programming");
	}
}

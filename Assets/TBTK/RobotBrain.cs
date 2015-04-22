using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RobotBrain : MonoBehaviour {

	private List<Command> coms;

	public void GoToProgram()
	{
		coms = new List<Command> ();
		foreach (Transform child in GameObject.Find ("Container").GetComponentInChildren<Transform>()) {
			if(child.GetComponent<Command>()){
				coms.Add(child.GetComponent<Command>());
			}
		}
		ExecuteAllComs ();
	}

	void ExecuteAllComs()
	{
		for (int i = 0; i < coms.Count; i++)
		{
			coms[i].Execute();
		}
	}
}

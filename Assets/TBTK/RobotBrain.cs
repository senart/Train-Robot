using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RobotBrain : MonoBehaviour {

	public List<Command> coms;

	public void SetCommands(List<Command> coms)
	{
		this.coms = coms;
	}

	void OnLevelWasLoaded(int level)
	{
		if (level == 3) {
			gameObject.AddComponent<MyUnit> ();
			ExecuteAllComs ();
		}
	}
	
	void ExecuteAllComs()
	{
		for (int i = 0; i < coms.Count; i++)
		{
			coms[i].Execute();
		}
	}
}

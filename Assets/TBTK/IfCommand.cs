using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IfCommand : Command 
{

	/*
	public override void UpdateCommands()
	{
		coms = new List<Command> ();
		foreach(Transform child in transform.Find("Commands").GetComponentInChildren<Transform>()){
			if(child.GetComponent<Command>()){
				coms.Add(child.GetComponent<Command>());
			}
		}
	}*/

	public override void Execute(RobotBrain playerBrain)
	{
		base.Execute(playerBrain);
		UpdateCommands ();
		if(myVar.GetBoolData()) {
			for(int i =0; i < coms.Count;i++){
				coms[i].Execute(playerBrain);
			}
		}
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ForCommand : Command {
	
	public List<Command> coms;
	
	public override void UpdateCommands()
	{
		coms = new List<Command> ();
		foreach(Transform child in transform.Find("Commands").GetComponentInChildren<Transform>()){
			if(child.GetComponent<Command>()){
				coms.Add(child.GetComponent<Command>());
			}
		}
	}
	
	public override void Execute(RobotBrain playerBrain)
	{
		UpdateCommands ();
		for (int i = 0; i < myVar.GetData(); i++) {
			for(int j =0; j < coms.Count;j++){
				coms[j].Execute(playerBrain);
			}
		}
	}
}

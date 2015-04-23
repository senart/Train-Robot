using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IfCommand : Command 
{
	public bool condition;
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

	public override void Execute()
	{
		UpdateCommands ();
		if(myVar.GetBoolData()) {
			for(int i =0; i < coms.Count;i++){
				coms[i].Execute();
			}
		}
	}
}

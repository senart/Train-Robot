using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Command : ProgramDropItem {
	
	public Variable myVar;
	public List<Command> coms;

	public virtual void Execute(RobotBrain playerBrain){
		myVar = thisContainer.getVariable();
		Debug.Log (myVar);
	}

	public void SetVariable (Variable dropped)
	{
		myVar = dropped;
	}

	public void UpdateCommands()
	{
		myVar = thisContainer.getVariable();
		coms = new List<Command> ();
		foreach(CommandContainerGUI child in thisContainer.children){
			Command command = child.gameObject.GetComponent<Command>();
			if(command){
				if (command is ForCommand || command is IfCommand) {
					command.UpdateCommands();
				}
				coms.Add(child.GetComponent<Command>());
			}
		}
	}
}

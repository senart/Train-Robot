using UnityEngine;
using System.Collections;

public class Command : ProgramDropItem {
	
	public Variable myVar;

	public virtual void Execute(RobotBrain playerBrain){
		//Nothing
	}

	public void SetVariable (Variable dropped)
	{
		myVar = dropped;
	}

	public virtual void UpdateCommands()
	{
		//Nothing
	}
}

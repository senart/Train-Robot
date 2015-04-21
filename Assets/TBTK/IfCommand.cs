using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IfCommand : Command 
{
	public bool condition= false;
	public Dictionary<int, Command> coms;

	public IfCommand(int ID) : base(ID)
	{
		coms = new Dictionary<int, Command> ();
	}

	public void SetVariable (Variable dropped)
	{
		condition=dropped.GetConditionData();
	}

	public void OnCommandsDrop(Command com)
	{
		coms.Add(com.GetID(), com);
	}

	public override void Execute()
	{
		if(condition) {
			for(int i =0; i < coms.Count;i++){
				coms[i].Execute();
			}
		}
	}

	protected override void RemoveFromCommands(int ID)
	{
		coms.Remove (ID);
	}

}

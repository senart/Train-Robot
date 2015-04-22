using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IfCommand : Command 
{
	public bool condition= false;
	public List<Command> coms;

	protected override void SetVariable (GameObject dropped)
	{
		condition = dropped.GetComponent<Variable>().GetConditionData();
	}

	public void OnCommandsDrop(Command com)
	{
		//coms.Add(com.GetID(), com);
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
		//coms.Remove (ID);
	}

}

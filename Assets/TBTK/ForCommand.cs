using UnityEngine;
using System.Collections;

public class ForCommand : Command {
	
	public override void Execute(RobotBrain playerBrain)
	{
		base.Execute(playerBrain);
		UpdateCommands ();
		for (int i = 0; i < myVar.GetData(); i++) {
			for(int j =0; j < coms.Count;j++){
				coms[j].Execute(playerBrain);
			}
		}
	}
}

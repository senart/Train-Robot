using UnityEngine;
using System.Collections;

public class MoveCommand : Command 
{
	private int numTiles;

	public override void Execute(RobotBrain playerBrain)
	{
		numTiles = myVar.GetData ();
		Debug.Log ("Move!");
		playerBrain.MovePlayerForward (numTiles);
	}
}

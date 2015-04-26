using UnityEngine;
using System.Collections;

public class TurnCommand : Command 
{
	private int degrees;

	public override void Execute(RobotBrain playerBrain){
		degrees = myVar.GetData ();
		Debug.Log ("Rotate!");
		playerBrain.RotatePlayer(degrees);
	}
}

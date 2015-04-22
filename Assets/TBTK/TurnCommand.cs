using UnityEngine;
using System.Collections;

public class TurnCommand : Command 
{
	private int degrees;
	
	public void SetVariable (Variable dropped)
	{
		degrees = dropped.GetData();
	}

	public override void Execute(){
		Debug.Log ("Rotate!");
		//Rotate Player (degrees);
	}

}

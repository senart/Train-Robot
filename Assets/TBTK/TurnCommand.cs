using UnityEngine;
using System.Collections;

public class TurnCommand : Command 
{
	private int degrees;

	public TurnCommand(int ID) : base(ID)
	{
		degrees = 0;
	}
	
	public void SetVariable (Variable dropped)
	{
		degrees = dropped.GetData();
	}

	public override void Execute(){
		Debug.Log ("Rotate!");
		//Rotate Player (degrees);
	}

}

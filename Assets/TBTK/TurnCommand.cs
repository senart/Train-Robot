using UnityEngine;
using System.Collections;

public class TurnCommand : Command 
{
	private int degrees;

	public override void Execute(){
		degrees = myVar.GetData ();
		Debug.Log ("Rotate!");
		GameObject.Find ("Player").GetComponent<MyUnit> ().RotatePlayer(degrees);
	}

}

using UnityEngine;
using System.Collections;

public class MoveCommand : Command 
{
	private int numTiles;

	public override void Execute()
	{
		numTiles = myVar.GetData ();
		Debug.Log ("Move!");
		GameObject.Find ("Player").GetComponent<MyUnit> ().MovePlayerForward (numTiles);
	}
}

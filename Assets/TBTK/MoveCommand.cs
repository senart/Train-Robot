using UnityEngine;
using System.Collections;

public class MoveCommand : Command 
{
	private int numTiles;

	public MoveCommand(int ID) : base(ID)
	{
		numTiles = 0;
	}

	public void SetVariable(Variable dropped)
	{
		numTiles = dropped.GetData ();
	}
	
	public override void Execute()
	{
		Debug.Log ("Move!");
		GameObject.Find ("Player").GetComponent<MyUnit> ().MovePlayerForward (numTiles);
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RobotBrain : MonoBehaviour 
{
	public float moveSpeed = 6F;
	public float rotationSpeed = 6F;

	private Quaternion facingRotation;

	int rows = 18;
	int columns = 14;
	int rowMod =0;
	int colMod = 1;
	
	MyTile tile;
	MyTile targetTile;
	MyTile[,] grid;

	public List<Command> coms;

	public void SetCommands(List<Command> coms)
	{
		this.coms = coms;
	}

	void OnLevelWasLoaded(int level)
	{
		if (level == 2) {
			grid = new MyTile[columns, rows];
			//Populate the MyTile grid and index each tile with col and row numbers
			int i = 0;
			foreach (Transform trans in GameObject.Find ("SquareGrid").GetComponentInChildren<Transform>()) {
				int column = i % columns;
				int row = (i - column) / columns;
				MyTile mt = trans.GetComponent<MyTile> ();
				mt.column = column;
				mt.row = row;
				grid [column, row] = mt;
				i++;
			}
			tile = grid [3, 3];  //starting tile
			transform.position = tile.GetPos ();
			ExecuteAllComs();
		}
	}

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.E)) {
			RotatePlayer(90);
		}
		if (Input.GetKeyDown (KeyCode.Q)) {
			MovePlayerForward(1);
		}
	}

	void ExecuteAllComs()
	{
		for (int i = 0; i < coms.Count; i++)
		{
			coms[i].Execute(this);
		}
	}

	public void MovePlayerForward(int tilesToGo)
	{
		StartCoroutine (_MovePlayerForward (tilesToGo));
	}

	private IEnumerator _MovePlayerForward (int tilesToGo)
	{
		while(!TurnManager.ClearToProceed()) yield return null;

		TurnManager.ActionCommenced ();

		//While we are within the range of the grid and have tiles to go
		while (tilesToGo>0) {
			//out of map boundry checks
			if((tile.column == columns && colMod == 1) ||
			   (tile.row == rows && rowMod ==1)) break;
			else if((tile.column == 0 && colMod == -1) ||
			   (tile.row == 0 && rowMod == -1)) break;
			targetTile = grid [tile.column + colMod, tile.row+rowMod];
			if(targetTile.isObstacle) break;

			//transform.position = targetTile.GetPos ();
			while(true){
				float dist=Vector3.Distance(transform.position, targetTile.GetPos());
				if(dist<0.05f) break;
				
				Quaternion wantedRot=Quaternion.LookRotation(targetTile.GetPos()-transform.position);
				transform.rotation=Quaternion.Slerp(transform.rotation, wantedRot, Time.deltaTime*moveSpeed*3);
				
				Vector3 dir=(targetTile.GetPos()-transform.position).normalized;
				transform.Translate(dir*Mathf.Min(moveSpeed*Time.deltaTime, dist), Space.World);
				yield return null;
			}
			tile = targetTile;
			tilesToGo--;
		}
		TurnManager.ActionCompleted();
		//return tilesToGo;
	}

	public void RotatePlayer(int degrees)
	{
		StartCoroutine(_Rotate(degrees));
	}
	
	IEnumerator _Rotate(int degrees)
	{
		while(!TurnManager.ClearToProceed()) yield return null;
		
		TurnManager.ActionCommenced ();

		if (degrees == 90) {
			if(rowMod == 0 && colMod == -1){
				rowMod = -1;
				colMod = 0;
			} else if(rowMod == -1 && colMod == 0){
				rowMod = 0;
				colMod = 1;
			} else if(rowMod == 0 && colMod == 1){
				rowMod = 1;
				colMod = 0;
			} else if(rowMod ==1 && colMod == 0){
				rowMod = 0;
				colMod = -1;
			}
		} else if (degrees == -90) {
			if(rowMod == 0 && colMod == -1){
				rowMod = 1;
				colMod = 0;
			} else if(rowMod == -1 && colMod == 0){
				rowMod = 0;
				colMod = -1;
			} else if(rowMod == 0 && colMod == 1){
				rowMod = -1;
				colMod = 0;
			} else if(rowMod ==1 && colMod == 0){
				rowMod = 0;
				colMod = 1;
			}
		}

		float rotDeg = Mathf.RoundToInt(transform.rotation.eulerAngles.y) + degrees;
		facingRotation = Quaternion.Euler (0, rotDeg, 0);
		while(Quaternion.Angle(facingRotation, transform.rotation)>0.4f){
			transform.rotation=Quaternion.Lerp(transform.rotation, facingRotation, Time.deltaTime*rotationSpeed*2);
			yield return null;
		}
		TurnManager.ActionCompleted();
	}
}

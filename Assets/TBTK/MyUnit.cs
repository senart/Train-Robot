using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class MyUnit : MonoBehaviour
{
	int rows = 18;
	int columns = 14;
	int rowMod =0;
	int colMod = 1;

	MyTile tile;
	MyTile targetTile;
	MyTile[,] grid;

	void Start ()
	{
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
		tile = grid [0, 0];
		transform.position = tile.GetPos ();

		Variable va = new Variable ();
		va.OnClick ();
		MoveCommand mov = new MoveCommand (0);
		mov.SetVariable (va);
		IfCommand test = new IfCommand (1);
		test.condition = true;
		test.OnCommandsDrop (mov);
		List<Command> cmds = new List<Command> ();
		cmds.Add (test);
		foreach (Command cmd in cmds) {
			cmd.Execute();
		}
	}

	public int MovePlayerForward (int tilesToGo)
	{
		//While we are within the range of the grid and have tiles to go
		while (tilesToGo>0) {
			if((tile.column == columns && colMod == 1) ||
			   (tile.row == rows && rowMod ==1)) break;
			if((tile.column == 0 && colMod == -1) ||
			   (tile.row == 0 && rowMod == -1)) break;
			targetTile = grid [tile.column + colMod, tile.row+rowMod];
			transform.position = targetTile.GetPos ();
			tile = targetTile;
			tilesToGo--;
		}
		return tilesToGo;
	}

	public void RotatePlayer(int degrees)
	{
		transform.Rotate (0, degrees, 0);
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
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			int spareTiles = MovePlayerForward (1);
		}
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			RotatePlayer(90);
		}
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			RotatePlayer(-90);
		}
	}
}
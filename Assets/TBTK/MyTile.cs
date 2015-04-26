using UnityEngine;
using System.Collections;

public class MyTile : MonoBehaviour {

	//public RobotBrain brain;
	public int ID;
	public int column;
	public int row;
	public bool isObstacle;

	public Vector3 GetPos(){
		return transform.position;
	}
}

using UnityEngine;
using System.Collections;

public class MyTile : MonoBehaviour {

	//public MyUnit unit;
	public int ID;
	public int column;
	public int row;

	public Vector3 GetPos(){
		return transform.position;
	}
}

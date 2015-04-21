using UnityEngine;
using System.Collections;

public class Variable : MonoBehaviour {
	private int data;
	private bool condition;

	public int GetData()
	{
		return data;
	}

	public bool GetConditionData()
	{
		//Open Tynker and check 0 = 0 ...
		return condition;
	}

	public void OnClick(){
		data = 17;
		//Display textbox for number input, select Left,Right from list (Left = -90, Right = 90)….
		// set condition to true...
	}
}

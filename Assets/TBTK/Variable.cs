using UnityEngine;
using System.Collections;

public class Variable : ProgramDropItem
{
	public int data;
	public bool condition;

	protected override void ActuallyDropItem (GameObject surface)
	{
		// Is there a droppable container?
		UIDragDropContainer container = surface ? NGUITools.FindInParents<UIDragDropContainer>(surface) : null;
		
		if (container != null)
		{
			//if dropped on a variable, that has a command parent
			if (surface.GetComponent<Variable> () && surface.transform.parent.GetComponent<Command> ()) {
				mTrans.parent = surface.transform.parent;
				mTrans.position = surface.transform.position;
				surface.transform.parent.GetComponent<Command> ().SetVariable (GetComponent<Variable> ());
				mDragging = true; //TO PREVENT DRAGS from already placed variable
				Destroy (surface);
			} else
				mTrans.parent = GameObject.Find ("Container").transform;
		}
		else
		{
			// No valid container under the mouse -- revert the item's parent
			mTrans.parent = mParent;
			NGUITools.Destroy(gameObject);
		}
	}

	public int GetData ()
	{
		return data;
	}

	public bool GetBoolData ()
	{
		return condition;
	}

	public void OnClick ()
	{
		//data = 1;
		//Display textbox for number input, select Left,Right from list (Left = -90, Right = 90)….
		// set condition to true...
	}
}

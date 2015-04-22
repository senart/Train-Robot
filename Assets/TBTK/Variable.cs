using UnityEngine;
using System.Collections;

public class Variable : UIDragDropItem
{

	private int data;
	private bool condition;
	private bool secondDrag;

	protected override void OnDragDropRelease (GameObject surface)
	{
		if (secondDrag)
			cloneOnDrag = true;
		if (cloneOnDrag) {
			mTouchID = int.MinValue;
			
			// Re-enable the collider
			if (mButton != null)
				mButton.isEnabled = true;
			else if (mCollider != null)
				mCollider.enabled = true;
			else if (mCollider2D != null)
				mCollider2D.enabled = true;
			
			// Is there a droppable container?
			UIDragDropContainer container = surface ? NGUITools.FindInParents<UIDragDropContainer> (surface) : null;
			
			if (container != null) {
				//if dropped on a variable, that has a command parent
				if (surface.GetComponent<Variable> () && surface.transform.parent.GetComponent<Command> ()) {
					mTrans.parent = surface.transform.parent;
					mTrans.position = surface.transform.position;
					surface.transform.parent.GetComponent<Command> ().SetVariable (GetComponent<Variable> ());
					mDragging = true; //TO PREVENT DRAGS from already placed variable
					Destroy (surface);
				} else
					mTrans.parent = GameObject.Find ("Container").transform;
			} else {
				// No valid container under the mouse -- revert the item's parent
				mTrans.parent = mParent;
			}
			
			// Update the grid and table references
			mParent = mTrans.parent;
			mGrid = NGUITools.FindInParents<UIGrid> (mParent);
			mTable = NGUITools.FindInParents<UITable> (mParent);
			
			// Re-enable the drag scroll view script
			if (mDragScrollView != null)
				StartCoroutine (EnableDragScrollView ());
			
			// Notify the widgets that the parent has changed
			NGUITools.MarkParentAsChanged (gameObject);
			
			if (mTable != null)
				mTable.repositionNow = true;
			if (mGrid != null)
				mGrid.repositionNow = true;

			//For subsequent drag/drop/clones
			secondDrag = true;
			// We're now done
			OnDragDropEnd ();
		} else
			NGUITools.Destroy (gameObject);
		cloneOnDrag = false;
	}

	public int GetData ()
	{
		data = 1;
		return data;
	}

	public bool GetBoolData ()
	{
		condition = true;
		return condition;
	}

	public void OnClick ()
	{
		//data = 1;
		//Display textbox for number input, select Left,Right from list (Left = -90, Right = 90)….
		// set condition to true...
	}
}

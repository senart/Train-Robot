using UnityEngine;
using System.Collections;

public class Command : UIDragDropItem {

	private bool secondDrag = false;

	protected override void OnDragDropRelease (GameObject surface)
	{
		if (secondDrag)
			cloneOnDrag = true;
		if (cloneOnDrag)
		{
			mTouchID = int.MinValue;
			
			// Re-enable the collider
			if (mButton != null) mButton.isEnabled = true;
			else if (mCollider != null) mCollider.enabled = true;
			else if (mCollider2D != null) mCollider2D.enabled = true;
			
			// Is there a droppable container?
			UIDragDropContainer container = surface ? NGUITools.FindInParents<UIDragDropContainer>(surface) : null;
			
			if (container != null)
			{
				if(gameObject.GetType().ToString() == "Variable") {
					if(container.GetComponent<IfCommand>() != null){
						mTrans.parent = container.transform;
						mTrans.position = container.transform.position;
						SetVariable(gameObject);
						Destroy(surface);
					}
				}

				// Container found -- parent this object to the container
				mTrans.parent = (container.reparentTarget != null) ? container.reparentTarget : container.transform;
				
				Vector3 pos = mTrans.localPosition;
				pos.z = 0f;
				mTrans.localPosition = pos;
			}
			else
			{
				// No valid container under the mouse -- revert the item's parent
				mTrans.parent = mParent;
			}
			
			// Update the grid and table references
			mParent = mTrans.parent;
			mGrid = NGUITools.FindInParents<UIGrid>(mParent);
			mTable = NGUITools.FindInParents<UITable>(mParent);
			
			// Re-enable the drag scroll view script
			if (mDragScrollView != null)
				StartCoroutine(EnableDragScrollView());
			
			// Notify the widgets that the parent has changed
			NGUITools.MarkParentAsChanged(gameObject);
			
			if (mTable != null) mTable.repositionNow = true;
			if (mGrid != null) mGrid.repositionNow = true;

			//For subsequent drag/drop/clones
			secondDrag = true;
			// We're now done
			OnDragDropEnd();
		}
		else NGUITools.Destroy(gameObject);
		cloneOnDrag = false;
	}

	public virtual void Execute(){
		//Nothing
	}

	protected virtual void MyOnDrag(){
		//if (transform.parent != null)
			//transform.parent.GetComponent<Command>().RemoveFromCommands (ID);
	}

	protected virtual void RemoveFromCommands (int ID){
		//Nothing
	}

	protected virtual void SetVariable(GameObject dropped){
		//do nothing
	}
}

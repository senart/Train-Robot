using UnityEngine;
using System.Collections;

public class ProgramDropItem : UIDragDropItem {

	private bool secondDrag = false;

	protected override void OnDragDropMove (Vector2 delta)
	{
		GameObject.Find ("DeleteBox").GetComponent<UISprite> ().depth = 3;
		mTrans.localPosition += (Vector3)delta;
	}

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
			
			ActuallyDropItem(surface);
			
			//For subsequent drag/drop/clones
			secondDrag = true;
			// We're now done
			OnDragDropEnd();
		}
		else NGUITools.Destroy(gameObject);
		cloneOnDrag = false;
		GameObject.Find ("DeleteBox").GetComponent<UISprite> ().depth = 0;
	}

	protected virtual void ActuallyDropItem(GameObject surface)
	{
		// Is there a droppable container?
		UIDragDropContainer container = surface ? NGUITools.FindInParents<UIDragDropContainer>(surface) : null;
		
		if (container != null)
		{
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
			NGUITools.Destroy(gameObject);
		}
	}
}

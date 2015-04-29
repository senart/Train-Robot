using UnityEngine;
using System.Collections;

public class ProgramDropItem : UIDragDropItem
{
	public CommandContainerGUI thisContainer;

	protected override void OnDragDropStart ()
	{
		Detach();
		base.OnDragDropStart ();
	}

	protected override void OnDragDropMove (Vector2 delta)
	{
		mTrans.localPosition += (Vector3)delta;
		thisContainer.dirty = true;
	}

	protected override void OnDragDropEnd (){
		thisContainer.dirty = true;
	}

	public void Attach (GameObject obj)
	{
		if (thisContainer.parent) Detach ();
		if (obj == gameObject) return;

		CommandContainerGUI otherContainer = obj.GetComponent<CommandContainerGUI>();
		if (!otherContainer) return;
		if (otherContainer.children.Contains(thisContainer)) return;

		thisContainer.root = otherContainer.root;
		thisContainer.parent = otherContainer;
		otherContainer.children.Add (thisContainer);
		thisContainer.root.dirty = true;
	}

	public void Detach ()
	{
		if (!thisContainer.parent) return;
		thisContainer.parent.children.Remove (thisContainer);
		thisContainer.root.dirty = true;
		thisContainer.root = thisContainer;
		thisContainer.parent = null;
	}

	protected override void OnDragDropRelease (GameObject surface)
	{
		GameObject lastHit = UICamera.lastHit.collider.gameObject;
		// dragged on a command container
		if (lastHit.tag == "CommandContainer") {
			//dragged on the bottom panel
			if (lastHit.transform.parent.parent.gameObject.GetComponent<CommandContainerGUI>().isMenuItem) {
				mTrans.parent = mParent;
				NGUITools.Destroy (gameObject);
				return;
			} else {
				Attach (lastHit.transform.parent.parent.gameObject);
			}
		}

		// Re-enable the collider
		if (mButton != null)
			mButton.isEnabled = true;
		else if (mCollider != null)
			mCollider.enabled = true;
		else if (mCollider2D != null)
			mCollider2D.enabled = true;

		// dragged on a scroll view
		UIDragDropContainer container = surface ? NGUITools.FindInParents<UIDragDropContainer> (surface) : null;
		
		if (container != null) {// Container found -- parent this object to the container
			mTrans.parent = (container.reparentTarget != null) ? container.reparentTarget : container.transform;

			if (cloneOnDrag) cloneOnDrag = false;

			// not snapped
			if (!thisContainer.parent) {	
				Vector3 pos = mTrans.localPosition;
				pos.z = 0f;
				mTrans.localPosition = pos;
			}

			thisContainer.isMenuItem = false;

		} else {
			// No valid container under the mouse -- revert the item's parent
			mTrans.parent = mParent;
			thisContainer.DestroyThisAndChildren();
		}
		//no idea why this is here :)
		mTouchID = int.MinValue;
	}
}

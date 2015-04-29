using UnityEngine;
using System.Collections;

public class Variable : UIDragDropItem
{
	public int data;
	public bool condition;

	public int GetData ()
	{
		return data;
	}

	public bool GetBoolData ()
	{
		return condition;
	}

	protected override void StartDragging ()
	{
		if (!mDragging)
		{
			if (cloneOnDrag)
			{
				if (gameObject.GetComponent<UIInput>()) {
					gameObject.GetComponent<UIInput>().isSelected = false;
					gameObject.GetComponent<UIInput>().RemoveFocus();
				}
				GameObject clone = NGUITools.AddChild (transform.parent.gameObject, gameObject);
				clone.transform.localPosition = transform.localPosition;
				clone.transform.localRotation = transform.localRotation;
				clone.transform.localScale = transform.localScale;
				
				UIButtonColor bc = clone.GetComponent<UIButtonColor>();
				if (bc != null) bc.defaultColor = GetComponent<UIButtonColor>().defaultColor;
				
				UICamera.currentTouch.dragged = clone;
				
				Variable item = clone.GetComponent<Variable>();
				item.mDragging = true;
				item.Start();
				item.OnDragDropStart();
			}
			else
			{
				mDragging = true;
				OnDragDropStart();
			}
		}
	}

	protected override void OnDragDropRelease (GameObject surface) {
		// Re-enable the collider
		if (mButton != null)
			mButton.isEnabled = true;
		else if (mCollider != null)
			mCollider.enabled = true;
		else if (mCollider2D != null)
			mCollider2D.enabled = true;

		GameObject lastHit = UICamera.lastHit.collider.gameObject;
		if ( lastHit.tag == "VariableContainer") {
			CommandContainerGUI commandContainer = lastHit.GetComponentInParent<CommandContainerGUI>();
			if (commandContainer) {
				commandContainer.SetVariable(gameObject);
				return;
			}
		}
		NGUITools.Destroy(gameObject);
	}
}

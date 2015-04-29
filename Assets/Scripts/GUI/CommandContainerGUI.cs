using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CommandContainerGUI : MonoBehaviour {
	public GameObject varParent;
	public UIWidget container, commandBlock;
	public bool dirty, isMenuItem=true;
	public Transform bottomAnchor, topAnchor, rightAnchor, leftAnchor;
	public int indentBetween;
	public Vector3 topIndent, bottomIndent, defaultSize = new Vector3(160, 120, 0);
	[HideInInspector]
	public CommandContainerGUI root, parent=null;
	public List<CommandContainerGUI> children = new List<CommandContainerGUI>();

	GameObject variable;

	void Start() {
		root=this;
	}

	void OnGUI(){
		if (dirty || true) {
			if (root) {
				root.alignChildren();
			} else { 
				alignChildren();
			}
		}
		GetComponent<BoxCollider>().center = leftAnchor.localPosition + rightAnchor.localPosition + topAnchor.localPosition + bottomAnchor.localPosition;
		GetComponent<BoxCollider>().size = new Vector3 (sizeX(), sizeY(), 0);

		Vector3 center = commandBlock.gameObject.GetComponent<BoxCollider>().center;
		commandBlock.gameObject.GetComponent<BoxCollider>().center = new Vector3(center.x, center.y, GetComponent<BoxCollider>().center.z-5);
		dirty = false;
	}

	public void SetVariable(GameObject var) {
		NGUITools.Destroy(varParent.transform.GetChild(0).gameObject);
		var.transform.SetParent(varParent.transform);
		var.transform.localPosition = Vector3.zero;
	}

	public Variable getVariable(){
		return varParent.GetComponentInChildren<Variable>();
	}

	//container size, recursive
	public int sizeY(int count=0){
		if (count>100) {
			DestroyThisAndChildren();
		}
		if (children.Count==0)
		return (int)(defaultSize).y;
		else {
			int y = 0;
			foreach (CommandContainerGUI child in children) {
				y+= child.sizeY(++count);
			}
			//between children
			y+=(int)Mathf.Abs(indentBetween*(children.Count-1));
			//above and below
			y+=(int)(Mathf.Abs(topIndent.y)+Mathf.Abs(bottomIndent.y));
			return y;
		}
	}
	public int sizeX(int count=0){
		if (count>100) {
			DestroyThisAndChildren();
		}
		if (children.Count==0)
		return (int)(defaultSize).x;
		else {
			int xMax = 0;
			foreach (CommandContainerGUI child in children) {
				int childSize = child.sizeX(++count);
				if (childSize>xMax) xMax = childSize;
			}
			return xMax + (int)(Mathf.Abs(topIndent.x)+Mathf.Abs(bottomIndent.x));
		}
	}

	public void alignChildren() {
		if (children.Count==0) {
			topAnchor.localPosition = new Vector3(0, defaultSize.y/2, 0);
			bottomAnchor.localPosition = new Vector3(0, -defaultSize.y/2, 0);
			rightAnchor.localPosition = new Vector3(defaultSize.x/2, 0, 0);
			leftAnchor.localPosition = new Vector3(-defaultSize.x/2, 0, 0);
			return;
		}
		bottomAnchor.localPosition = new Vector3 (0, -sizeY()/2,0);
		topAnchor.localPosition = new Vector3 (0, sizeY()/2, 0);
		rightAnchor.localPosition = new Vector3 (sizeX()/2, 0, 0);
		leftAnchor.localPosition = new Vector3 (-sizeX()/2, 0, 0);
		int height = (int)topIndent.y;
		foreach (CommandContainerGUI child in children) {
			child.alignChildren();
		}
		foreach (CommandContainerGUI child in children) {
			child.gameObject.transform.localPosition = gameObject.transform.localPosition+new Vector3(0,0,-30);
			height += child.sizeY()/2;
			child.gameObject.transform.localPosition += childCenterOffset(height);
			height += child.sizeY()/2;
			height += indentBetween;
		}
	}

	Vector3 childCenterOffset(int height) {
		int centerY = sizeY();
		return new Vector3((topIndent.x-bottomIndent.x)/2, - height + centerY/2, 0);
	}

	public void DestroyThisAndChildren(){
		parent = null;
		root = this;

		for (int i=0; i<children.Count; i++){
			children[i].DestroyThisAndChildren();
			children.RemoveAt(i);
		}

		if (children.Count==0)
		DestroyImmediate(gameObject);
	}

}

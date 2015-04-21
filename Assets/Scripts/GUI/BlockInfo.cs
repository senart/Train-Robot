using UnityEngine;
using System.Collections;

public class BlockInfo : MonoBehaviour {

	public string moduleName, tooltipTitle;
	public UILabel mName, tDescription, tTitle;

	void Start() {
		mName.text = moduleName;
//		tDescription.text = tooltipText;
		tTitle.text = tooltipTitle;
	}
}

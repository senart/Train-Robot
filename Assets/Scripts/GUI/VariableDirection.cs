using UnityEngine;
using System.Collections;

public class VariableDirection : MonoBehaviour {
	
	public Variable varScript;
	public UILabel label;
	
	void Start() {
		SetText();
	}
	
	public void OnClick() {
		varScript.data = -varScript.data;
		SetText();
	}
	
	void SetText() {
		label.text = varScript.data == 90 ? "Right" : "Left";
		label.color = varScript.data == 90 ? Color.blue : Color.yellow;
	}
	
}

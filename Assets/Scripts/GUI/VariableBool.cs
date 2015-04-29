using UnityEngine;
using System.Collections;

public class VariableBool : MonoBehaviour {

	public Variable varScript;
	public UILabel label;

	void Start() {
		SetText();
	}

	public void OnClick() {
		varScript.condition = !varScript.condition;
		SetText();
	}

	void SetText() {
		label.text = varScript.condition ? "True" : "False";
		label.color = varScript.condition ? Color.green : Color.red;
	}

}

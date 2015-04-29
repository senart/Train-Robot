using UnityEngine;
using System.Collections;
using System;

public class VariableNumber : MonoBehaviour {

	public int defaultValue;
	public Variable varScript;
	public UIInput input;

	public void Submit(){
		try {
			int inp = Int32.Parse(input.value);
			varScript.data = inp;
		}
		catch (Exception e) {
			varScript.data = defaultValue;
		}
	}

	public void OnGUI() {
		Submit();
	}
}

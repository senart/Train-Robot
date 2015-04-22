using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProgramBar : MonoBehaviour {

	public GameObject[] views;

	void Start(){
		if(views.Length>0) Show (0);
	}

	public void ControlsClick(){
		Show (0);
	}

	public void VariablesClick(){
		Show (1);
	}

	public void LocomotionClick(){
		Show (2);
	}

	void Show(int n){
		views [n].SetActive (true);
		for (int i = 0; i < views.Length; i++) {
			if(i!=n) views[i].SetActive(false);
		}
	}
}

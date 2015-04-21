using UnityEngine;
using System.Collections;

public class OnOffOnClick : MonoBehaviour {

	public bool showInitially;

	void Start(){
		gameObject.SetActive(showInitially);
	}

	public void OnClick() {
		gameObject.SetActive(!gameObject.activeSelf);
	}
}

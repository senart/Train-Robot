using UnityEngine;
using System.Collections;

public class Resolution : MonoBehaviour {

	// Use this for initialization
	private UILabel lbl;
	void Start () {
		lbl = GetComponent<UILabel>();
		lbl.text = Screen.width+" x "+Screen.height;
	}
	
	// Update is called once per frame
	void Update () {
		lbl.text = Screen.width+" x "+Screen.height;
	}
}

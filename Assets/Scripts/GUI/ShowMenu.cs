using UnityEngine;
using System.Collections;

public class ShowMenu : MonoBehaviour {

	public GameObject menuButtons;

	void OnClick () {
		menuButtons.SetActive(true);
		Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

using UnityEngine;
using System.Collections;

public class FireButton : MonoBehaviour
{
	bool press = false;

	void OnPress (bool pressed)
	{
		press = pressed;
	}

	void Update()
	{
		if (press) {
			foreach (Shoot shootScript in GameObject.Find("Player").GetComponentsInChildren<Shoot>()) {
				shootScript.Fire ();
			}
		}
	}
}

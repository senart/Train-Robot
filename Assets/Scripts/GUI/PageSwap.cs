using UnityEngine;
using System.Collections;

public class PageSwap : MonoBehaviour {

	public GameObject[] pages;
	public int current = 0;

 	// Use this for initialization
	void Start () {
		Show ();
	}

	public void NextPage(){
		current++;
		Show ();
	}

	public void PreviousPage(){
		current--;
		Show();
	}

	public void Show () {
		current = current % pages.Length;
		foreach (GameObject page in pages) {
			page.SetActive(false);
		}
		if (pages.Length==0) return;
		pages[current].SetActive(true);
	}
}

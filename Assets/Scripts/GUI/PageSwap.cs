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
		current = mod (current, pages.Length);
		foreach (GameObject page in pages) {
			page.SetActive(false);
		}
		if (pages.Length==0) return;
		pages[current].SetActive(true);
	}

	int mod(int x, int m) {
		int r = x%m;
		return r<0 ? r+m : r;
	}
}

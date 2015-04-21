using UnityEngine;
using System.Collections;

public class ShowStats : MonoBehaviour {

	Stats playerStats;

	// Use this for initialization
	void Start () {
		playerStats = GameObject.Find ("Player").GetComponent<Stats> ();
		UpdateStats ();
	}

	public void UpdateStats()
	{
		if (playerStats == null)
			return;
		GetComponent<UILabel> ().text = "CONTRAPTION\n";
		GetComponent<UILabel> ().text += playerStats.speed + " SPEED\n";
		GetComponent<UILabel> ().text += playerStats.damageBonus + " DAMAGE BONUS\n";
		GetComponent<UILabel> ().text += playerStats.protectionBonus + " PROTECTION BONUS\n";
		GetComponent<UILabel> ().text += playerStats.HPBonus + " HP BONUS";
	}
}

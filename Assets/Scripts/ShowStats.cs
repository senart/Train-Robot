using UnityEngine;
using System.Collections;

public class ShowStats : MonoBehaviour {

	Stats playerStats;
	public TooltipLabelPair speed, damage, protection, hp;
	public Color textC, speedC, damageC, protectionC, hpC;

	// Use this for initialization
	void Start () {
		playerStats = GameObject.Find ("Player").GetComponent<Stats> ();
		UpdateStats ();
	}

	public void UpdateStats()
	{
		if (playerStats){
			speed.right.text = "+" + playerStats.speedBonus.ToString();
			speed.left.text = "Speed:";
			speed.right.color = speedC;
			damage.left.text = "Damage:";
			damage.right.text = "+" + playerStats.damageBonus.ToString();
			damage.right.color = damageC;
			protection.left.text = "Protection:";
			protection.right.text = "+" + playerStats.protectionBonus.ToString();
			protection.right.color = protectionC;
			hp.left.text = "HP:";
			hp.right.text = "+" + playerStats.HPBonus.ToString();
			hp.right.color = hpC;
			speed.left.color = damage.left.color = protection.left.color = hp.left.color = textC;
		}
	}
}

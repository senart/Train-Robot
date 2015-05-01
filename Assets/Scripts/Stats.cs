using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour
{
	public int speed = 20;
	public int speedBonus;
	public int damageBonus;
	public int protectionBonus;
	public int HPBonus;

	public void AddToStats(Stats moduleStats)
	{
		speed += moduleStats.speedBonus;
		speedBonus += moduleStats.speedBonus;
		damageBonus += moduleStats.damageBonus;
		protectionBonus += moduleStats.protectionBonus;
		HPBonus += moduleStats.HPBonus;
		UpdateAllStats ();
	}
	
	public void RemoveFromStats(Stats moduleStats)
	{
		speed -= moduleStats.speedBonus;
		speedBonus -= moduleStats.speedBonus;
		damageBonus -= moduleStats.damageBonus;
		protectionBonus -= moduleStats.protectionBonus;
		HPBonus -= moduleStats.HPBonus;
		UpdateAllStats ();
	}

	private void UpdateAllStats()
	{
		if(GameObject.FindObjectOfType<ShowStats>())GameObject.FindObjectOfType<ShowStats> ().UpdateStats ();  //Update the label that is diplaying the stats
		/*foreach (Transform child in transform.GetComponentInChildren<Transform>()) {
			child.GetComponent<ModuleStats>().UpdateBonusStats(speedBonus, damageBonus,  protectionBonus,  HPBonus);
		}*/
		foreach (ModuleStats stats in GetComponentsInChildren<ModuleStats>()) {
			stats.UpdateBonusStats(speedBonus, damageBonus, protectionBonus, HPBonus);
		}
	}
}

using UnityEngine;
using System.Collections;

public class ShowModuleStats : MonoBehaviour {
	
	public void UpdateStats(ModuleStats moduleStats)
	{
		GetComponent<UILabel> ().text = moduleStats.name + "\n";
		GetComponent<UILabel> ().text += moduleStats.speed + " SPEED\n";
		GetComponent<UILabel> ().text += moduleStats.damage + " DAMAGE\n";
		GetComponent<UILabel> ().text += moduleStats.protection + " PROTECTION\n";
		GetComponent<UILabel> ().text += moduleStats.HP + " HP";
	}
}

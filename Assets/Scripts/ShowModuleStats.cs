using UnityEngine;
using System.Collections;

public class ShowModuleStats : MonoBehaviour {

	void Start()
	{
		UpdateStats (GameObject.Find("Main Module").GetComponent<ModuleStats> ());
	}

	public void UpdateStats(ModuleStats moduleStats){
		if (moduleStats != null)
			GetComponent<UILabel> ().text = "Total Effects ons Selected Module:\n";
			GetComponent<UILabel> ().text += moduleStats.moduleName + "\n";
			GetComponent<UILabel> ().text += moduleStats.speed + " SPEED\n";
			GetComponent<UILabel> ().text += moduleStats.damage + " DAMAGE\n";
			GetComponent<UILabel> ().text += moduleStats.protection + " PROTECTION\n";
			GetComponent<UILabel> ().text += moduleStats.HP + " HP";
	}
}

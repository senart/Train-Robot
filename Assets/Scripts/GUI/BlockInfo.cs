using UnityEngine;
using System.Collections;

public class BlockInfo : MonoBehaviour {

	public Vector3 rotation;
	public ModuleStats stats;
	public ModuleDescriptionLabels labels;

	void Start() {
		SetLabels();
	}

	void SetLabels() {
		labels.SHP.value = (int)stats.HP;
		labels.SDef.value = (int)stats.protection;
		labels.SDmg.value = (int)stats.damage;

		labels.BHP.value = (int)stats.HPBonus;
		labels.BDef.value = (int)stats.protectionBonus;
		labels.BDmg.value = (int)stats.damageBonus;
		labels.BSpeed.value = (int)stats.speedBonus;

		labels.description = stats.moduleDescription;
		labels.DTitle = stats.moduleName;
		labels.UpdateLabels();
	}
}

using UnityEngine;
using System.Collections;

public class BlockInfo : MonoBehaviour {

	public Vector3 rotation;
	public GameObject currentModule;

	ModuleDescriptionLabels labels;
	ModuleStats stats;
	void Start() {
		currentModule.AddComponent(typeof(Rotate));
		currentModule.GetComponent<Rotate>().rotation = this.rotation;
		labels = GetComponentInChildren<ModuleDescriptionLabels>();
		stats = currentModule.GetComponent<ModuleStats>();
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

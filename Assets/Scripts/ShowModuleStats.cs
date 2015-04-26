using UnityEngine;
using System.Collections;

public class ShowModuleStats : MonoBehaviour {

	public UILabel nameLabel;
	public TooltipLabelPair speed, damage, protection, hp;
	public Color textC, speedC, damageC, protectionC, hpC;
	public TweenAlpha tweenStart, tweenUpdate;

	ModuleStats moduleStats;

	void Start()
	{
		moduleStats = GameObject.Find("Main Module").GetComponent<ModuleStats> ();
		SetText();
		tweenStart.PlayForward();
	}

	public void UpdateStats(ModuleStats moduleStats){
		if (this.moduleStats == moduleStats) return;
		this.moduleStats = moduleStats;
		StartCoroutine(UpdateLabels());
	}

	IEnumerator UpdateLabels() {
		tweenUpdate.ResetToBeginning();
		tweenUpdate.PlayForward();
		yield return new WaitForSeconds(tweenUpdate.duration);
		tweenStart.ResetToBeginning();
		tweenStart.PlayForward();
		SetText();
	}

	void SetText(){
		if (moduleStats){
			nameLabel.text = moduleStats.moduleName;
			speed.right.text = moduleStats.speed.ToString();
			speed.left.text = "Speed:";
			speed.right.color = speedC;
			damage.left.text = "Damage:";
			damage.right.text = moduleStats.damage.ToString();
			damage.right.color = damageC;
			protection.left.text = "Protection:";
			protection.right.text = moduleStats.protection.ToString();
			protection.right.color = protectionC;
			hp.left.text = "HP:";
			hp.right.text = moduleStats.HP.ToString();
			hp.right.color = hpC;
			speed.left.color = damage.left.color = protection.left.color = hp.left.color = textC;
		}
	}
}

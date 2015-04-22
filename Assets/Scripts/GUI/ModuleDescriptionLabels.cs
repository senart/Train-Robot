using UnityEngine;
using System.Collections;

public class ModuleDescriptionLabels : MonoBehaviour {
	
	public string STitle;
	public Color STitleC;
	public string BTitle ;
	public Color BTitleC;
	public string DTitle;
	public Color DTitleC;
	public string description;
	public Color descriptionC;

	[System.Serializable]
	public class StringColorInt{
		public string name;
		public Color color, vColor;
		public int value;
	}

	public StringColorInt SHP, SDmg, SDef, BHP, BDmg, BDef, BSpeed;
	public UILabel LSTitle, LBTitle, LDTitle, LDescription;
	public TooltipLabelPair lSHP, lSDmg, lSDef, lBHP, lBDmg, lBDef, lBSpeed;

	public void Start() {
		UpdateLabels();
	}

	public void UpdateLabels () {
		LSTitle.text = STitle;
		LSTitle.color = STitleC;
		LBTitle.text = BTitle;
		LBTitle.color = BTitleC;
		LDTitle.text = DTitle;
		LDTitle.color = DTitleC;

		LDescription.text = description;
		LDescription.color = descriptionC;

		lSHP.Set(SHP, false);
		lSDmg.Set (SDmg, false);
		lSDef.Set (SDef, false);
		lBHP.Set (BHP, true);
		lBDef.Set(BDef, true);
		lBSpeed.Set(BSpeed, true);
		lBDmg.Set(BDmg, true);
	}
}

using UnityEngine;
using System.Collections;

public class TooltipLabelPair : MonoBehaviour {

	public UILabel left, right;

	[HideInInspector]
	public string leftText, rightText;
	[HideInInspector]
	public Color leftColor, rightColor;

	public void UpdateLabels () {
		left.text = leftText;
		left.color = leftColor;
		right.text = rightText;
		right.color = rightColor;
	}

	public void Set(string left, string right, Color leftColor, Color rightColor) {
		leftText = left; this.leftColor = leftColor;
		rightText = right; this.rightColor = rightColor;
		UpdateLabels();
	}

	public void Set(ModuleDescriptionLabels.StringColorInt data, bool showPlus) {
		string value = showPlus ? "+" + data.value.ToString() : data.value.ToString();
		this.Set (data.name, value, data.color, data.vColor);
	}
}

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
}

using UnityEngine;
using System.Collections;

public class GoToScene : MonoBehaviour {

	public string nameOfTheScene;
	public SceneFadeInOut fader;
	
	public void OnClick() {
		if (fader) {
			fader.nextSceneName = nameOfTheScene;
			fader.EndScene();
		} else {
			Application.LoadLevel(nameOfTheScene);
		}
	}
}

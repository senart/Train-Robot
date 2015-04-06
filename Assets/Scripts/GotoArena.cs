using UnityEngine;
using System.Collections;

public class GotoArena : MonoBehaviour {

	public GameObject prefab;

	void OnClick ()
	{
		//Add box colliders to Player
		foreach (Transform childTrans in GameObject.Find("Player").GetComponentInChildren<Transform>()) {
			childTrans.gameObject.AddComponent<SphereCollider>().isTrigger = true;
			SphereCollider sphere = GameObject.Find("Player").AddComponent<SphereCollider>();
			sphere.center=childTrans.position;
			Destroy(childTrans.GetComponent<BoxCollider>());
		}

		GameObject player = GameObject.Find ("Player");
		player.transform.position = new Vector3 (0, 0, 0);
		DontDestroyOnLoad (player);  //keep contraption

		Destroy(GameObject.FindGameObjectWithTag ("Cube Frame"));  //destroy frame

		Application.LoadLevel (1);
	}
}

using UnityEngine;
using System.Collections;

public class GotoArena : MonoBehaviour {

	public GameObject prefab;

	void OnClick ()
	{
		GameObject player = GameObject.Find ("Player");

		//Add Sphere Colliders to Player and remove the Box Colliders
		// Also +1 to the Mass of the RigidBody
		foreach (Transform childTrans in player.GetComponentInChildren<Transform>()) {
			childTrans.gameObject.AddComponent<SphereCollider>().isTrigger = true;
			SphereCollider sphere = player.AddComponent<SphereCollider>();
			sphere.center=childTrans.position;
			Destroy(childTrans.GetComponent<BoxCollider>());
			player.GetComponent<Rigidbody>().mass++;
		}
		
		player.transform.position = new Vector3 (0, 0, 0);
		DontDestroyOnLoad (player);  //keep contraption

		Destroy(GameObject.FindGameObjectWithTag ("Cube Frame"));  //destroy frame

		Application.LoadLevel ("control");
	}
}

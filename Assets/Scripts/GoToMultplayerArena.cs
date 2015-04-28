using UnityEngine;
using System.Collections;

public class GoToMultplayerArena : MonoBehaviour {

	void OnClick()
	{
		GameObject player = GameObject.Find("Player");
		//Add Sphere Colliders to Player and remove the Box Colliders
		// Also +1 to the Mass of the RigidBody
		foreach (Transform childTrans in player.GetComponentInChildren<Transform>()) {
			childTrans.gameObject.AddComponent<SphereCollider>().isTrigger = true;
			player.AddComponent<SphereCollider>().center = childTrans.position;
			Destroy(childTrans.GetComponent<BoxCollider>());
			player.GetComponent<Rigidbody>().constraints =  RigidbodyConstraints.FreezeRotationX |
															RigidbodyConstraints.FreezeRotationZ |
															RigidbodyConstraints.FreezePositionY;
			player.GetComponent<Rigidbody>().centerOfMass = new Vector3(0,0,0);
			//Solves a bug where Halo spams erros if it's enabled beforehand. Later, use different textures instead of Halos
			Behaviour halo = (childTrans.GetComponent("Halo") as Behaviour);
			if (halo != null) halo.enabled = false; 
		}
		
		player.transform.position = new Vector3 (0, 0, 0);
		DontDestroyOnLoad (player);  //keep contraption

		Destroy(GameObject.FindGameObjectWithTag ("Cube Frame"));  //destroy frame
		Application.LoadLevel ("NetworkScene");  //Switch scenes
	}
}

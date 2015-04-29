using UnityEngine;
using System.Collections;

public class BuildYourself : Photon.MonoBehaviour {
	void Awake()
	{
		object[] data = photonView.instantiationData;
		//Needs Awake, but Awake spams erros so here's a check
		if (data != null) {
			int parentViewID = (int)data [0];
			GameObject parentPlayer = PhotonView.Find (parentViewID).gameObject;

			//Add to the correct player, do other stuff...
			transform.parent = parentPlayer.transform;
			gameObject.AddComponent<SphereCollider> ().isTrigger = true;
			parentPlayer.AddComponent<SphereCollider> ().center = transform.position;
			Destroy (gameObject.GetComponent<BoxCollider> ());
			parentPlayer.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeRotationX |
				RigidbodyConstraints.FreezeRotationZ |
				RigidbodyConstraints.FreezePositionY;
			parentPlayer.GetComponent<Rigidbody> ().centerOfMass = new Vector3 (0, 0, 0);
		}
	}
}

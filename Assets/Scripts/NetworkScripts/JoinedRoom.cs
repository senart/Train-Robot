using UnityEngine;
using System.Collections;

public class JoinedRoom : MonoBehaviour {

	GameObject newPlayerObject;

    void OnJoinedRoom()
    {
		CreatePlayerObject ();
	}
	
	void CreatePlayerObject()
	{
		Vector3 pos = new Vector3( 0F,0F,0F);
		newPlayerObject = PhotonNetwork.Instantiate( "MrPlayer", pos, Quaternion.identity, 0 );
		foreach (Transform trans in GameObject.Find("TESTER").GetComponentInChildren<Transform>()) {
			GameObject temp = PhotonNetwork.Instantiate (trans.name, trans.position, trans.rotation, 0);
			temp.transform.parent = newPlayerObject.transform;
			Debug.Log(temp.transform.parent);
			//this.m_PhotonView.RPC ("Inst", PhotonTargets.All, m_PhotonView.viewID, trans.name,trans.position,trans.rotation);
			//GameObject pref = PhotonNetwork.Instantiate (name, loc, rot, 0);
		}

		newPlayerObject.AddComponent<Move> ();
		//Destroy (GameObject.Find ("TESTER"));
		
		Camera.main.gameObject.AddComponent<CameraFollow> ().targetMove = newPlayerObject.transform;
	}
	
}

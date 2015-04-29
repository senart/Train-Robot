using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerNetwork : Photon.MonoBehaviour
{

	//ThirdPersonCamera cameraScript;
	List<ModuleInfo> moduleInfo;
	Move moveScript;
	
	void Awake ()
	{
		moduleInfo = NetworkMenu.moduleInfo;
		//cameraScript = GetComponent<ThirdPersonCamera>();
		moveScript = GetComponent<Move> ();
		
		if (photonView.isMine) {
			//MINE: local player, simply enable the local scripts

			Camera.main.GetComponent<CameraFollow>().targetMove = transform;
			moveScript.enabled = true;

			object[] d = new object[1];
			d [0] = photonView.viewID;
			foreach (ModuleInfo module in moduleInfo) {
				GameObject temp = PhotonNetwork.Instantiate (module.name, module.pos, module.rot, 0, d);
			}
			GameObject.Find ("Stats").GetComponent<ShowStats>().playerStats = gameObject.GetComponent<Stats>();
		} else {

			//cameraScript.enabled = false;
			moveScript.enabled = true;
			moveScript.isControllable = false;
		}
		gameObject.name = gameObject.name + photonView.viewID;
	}
}
using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour
{
	
	private const string typeName = "UniqueGameName";
	private const string gameName = "RoomName";
	private HostData[] hostList;
	GameObject player;

	public string connectionIP = "83.143.253.215";
	public int connectionPort = 25001;
	
	void OnGUI()
	{
		if (Network.peerType == NetworkPeerType.Disconnected)
		{
			GUI.Label(new Rect(10, 10, 300, 20), "Status: Disconnected");
			if (GUI.Button(new Rect(10, 30, 120, 20), "Client Connect"))
			{
				Network.Connect(connectionIP, connectionPort);
			}
			if (GUI.Button(new Rect(10, 50, 120, 20), "Initialize Server"))
			{
				Network.InitializeServer(32, connectionPort, false);
			}
		}
		else if (Network.peerType == NetworkPeerType.Client)
		{
			GUI.Label(new Rect(10, 10, 300, 20), "Status: Connected as Client");
			if (GUI.Button(new Rect(10, 30, 120, 20), "Disconnect"))
			{
				Network.Disconnect(200);
			}
		}
		else if (Network.peerType == NetworkPeerType.Server)
		{
			GUI.Label(new Rect(10, 10, 300, 20), "Status: Connected as Server");
			if (GUI.Button(new Rect(10, 30, 120, 20), "Disconnect"))
			{
				Network.Disconnect(200);
			}
		}
	}

	
	void OnServerInitialized()
	{
		transportPlayer ();
	}
	
	
	void OnConnectedToServer()
	{
		transportPlayer ();
	}


	void transportPlayer(){

		GameObject player = GameObject.Find("Player");
		//Add Sphere Colliders to Player and remove the Box Colliders
			// Also +1 to the Mass of the RigidBody
		foreach (Transform childTrans in player.GetComponentInChildren<Transform>()) {
			childTrans.gameObject.AddComponent<SphereCollider>().isTrigger = true;
			SphereCollider sphere = player.AddComponent<SphereCollider>();
			sphere.center=childTrans.position;
			Destroy(childTrans.GetComponent<BoxCollider>());
			player.GetComponent<Rigidbody>().mass++;
			//Solves a bug where Halo spams erros if it's enabled beforehand. Later, use different textures instead of Halos
			Behaviour halo = (childTrans.GetComponent("Halo") as Behaviour);
			if (halo != null) halo.enabled = false; 
		}
		
		player.transform.position = new Vector3 (0, 0, 0);
		DontDestroyOnLoad (player);  //keep contraption
		
		Destroy(GameObject.FindGameObjectWithTag ("Cube Frame"));  //destroy frame

		Application.LoadLevel("control");
	}
}



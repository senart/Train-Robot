using UnityEngine;
using System.Collections;

public class ShowStatusWhenConnecting : MonoBehaviour 
{
    void Update()
	{
		GetComponent<UILabel> ().text = "Connecting" + GetConnectingDots () + "\n"
           + "Status: " + PhotonNetwork.connectionStateDetailed;

        if( PhotonNetwork.connectionStateDetailed == PeerState.Joined )
        {
			Destroy(gameObject);
        }
    }

    string GetConnectingDots()
    {
        string str = "";
        int numberOfDots = Mathf.FloorToInt( Time.timeSinceLevelLoad * 3f % 4 );

        for( int i = 0; i < numberOfDots; ++i )
        {
            str += " .";
        }

        return str;
    }
}

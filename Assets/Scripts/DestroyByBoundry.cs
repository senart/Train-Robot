using UnityEngine;
using System.Collections;

public class DestroyByBoundry : MonoBehaviour {

	void OnTriggerExit(Collider other)
	{
		Destroy (other.gameObject);
	}
}

using UnityEngine;
using System.Collections;

public class Plasma : MonoBehaviour {

	public float speed;
	public int damage;

	// get the damage from the shooter parent, become parantless and fire away...
	public void FireAway () {
		damage = transform.parent.GetComponent<ModuleStats> ().damage;
		transform.parent = null;
		GetComponent<Rigidbody> ().velocity = transform.forward * speed;
	}
}

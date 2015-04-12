using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate = 0.25F;
	public bool shootNonStop = false;  //For testing purposes

	float nextFire=0.0F;
	bool loadedLevel = false;

	void OnLevelWasLoaded(int loadLevel)
	{
		if (loadLevel == 1)
			loadedLevel = true;
	}

	void FixedUpdate()
	{
		if (shootNonStop) {
			Fire ();
		}
	}

	public void Fire()
	{
		if (Time.time > nextFire && loadedLevel) { 
			nextFire = Time.time + fireRate;
			//Instantiate the prefab, add it as a child to the Shooter Module to get the damage data
			//and fire it away from it's Plasma scrip
			GameObject prefabInstance = (GameObject)Instantiate (shot, shotSpawn.position,shotSpawn.rotation);
			prefabInstance.transform.parent=transform;
			prefabInstance.GetComponent<Plasma>().FireAway();
		}
	}
}

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
			Instantiate (shot, shotSpawn.position,shotSpawn.rotation);
		}
	}
}

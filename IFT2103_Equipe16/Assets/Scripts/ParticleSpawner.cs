using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour {

	ParticlePooler particlePooler;
	//public bool spawnParticle = false;
	public float cadence = 1f;
	private bool isSpawning =true;

	private void Start()
	{
		particlePooler = ParticlePooler.Instance;
		StartCoroutine(Wait(cadence));
	}

	void FixedUpdate()
	{
		if (isSpawning)
		{
			particlePooler.SpawnFromPool("Particle", transform.position, Quaternion.identity);
			isSpawning = false;
			StartCoroutine(Wait(cadence));
		}
	}

	IEnumerator Wait(float time)
	{
		yield return new WaitForSeconds(time);
		isSpawning = true;
	}


	void OnDestroy()
	{
		particlePooler.StopSpawning("particleFireBall");
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePooler : MonoBehaviour {


	public List<Pool> pools;
	public Dictionary<string, Queue<GameObject>> poolDictionary;
	public static ParticlePooler Instance;

	private void Awake()
	{
		Instance = this;
	}

	[System.Serializable]
	public class Pool
	{
		public string tag;
		public GameObject prefab;
		public int size;
	}


	void Start() {
		poolDictionary = new Dictionary<string, Queue<GameObject>>();
		foreach (Pool pool in pools)
		{
			Queue<GameObject> objectPool = new Queue<GameObject>();
			for (int i = 0; i < pool.size; i++)
			{
				GameObject obj = Instantiate(pool.prefab);
				obj.SetActive(false);
				objectPool.Enqueue(obj);
			}
			poolDictionary.Add(pool.tag, objectPool);
		}
	}

	public GameObject SpawnFromPool(string tag, Vector3 positionSpawn, Quaternion rotationSpawn)
	{
		GameObject particleToSpawn = poolDictionary[tag].Dequeue();
		particleToSpawn.SetActive(true);
		particleToSpawn.transform.position = positionSpawn;
		particleToSpawn.transform.rotation = rotationSpawn;
		poolDictionary[tag].Enqueue(particleToSpawn);
		return particleToSpawn;
	}

	public void StopSpawning(string tag)
	{
		GameObject[] particles = GameObject.FindGameObjectsWithTag(tag);
		foreach(GameObject particle in particles)
		{
			particle.SetActive(false);
		}
	}
	

}

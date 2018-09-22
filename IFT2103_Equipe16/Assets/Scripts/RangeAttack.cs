using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : MonoBehaviour {
	private Rigidbody projectile;
	public string nameOfTargets;
	public float lifeTime = 5f;
	private Transform target;
	public float verticalMaxDisplacement = 2f;
	[Range(-0.01f,-100)]
	public float gravity = -18f;
	private GameObject gameManager;

	void Start()
	{
		gameManager = GameObject.FindGameObjectWithTag("Game manager");
		Invoke("DestroyProjectile", lifeTime);
		projectile = gameObject.GetComponent<Rigidbody>();
		projectile.useGravity = false;
		GameObject[] Targets = GameObject.FindGameObjectsWithTag(nameOfTargets);
		int nbTargets = Targets.Length;
		target = Targets[Random.Range(0,nbTargets)].transform;
		Launch();
	}

	//lancer le projectile
	void Launch()
	{
		Physics.gravity = Vector3.up * gravity;
		projectile.useGravity = true;
		projectile.velocity = CalcLaunchVelocity();
	}
	//calculer la vitesse du lancer
	Vector3 CalcLaunchVelocity()
	{
		float displacementY = target.position.y - projectile.position.y;
		Vector3 displacementXZ = new Vector3(target.position.x - projectile.position.x, 0, target.position.z - projectile.position.z);
		Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * verticalMaxDisplacement);
		float time = Mathf.Sqrt(-2 * verticalMaxDisplacement / gravity) + Mathf.Sqrt(2 * (displacementY - verticalMaxDisplacement) / gravity);
		Vector3 velocityXY = displacementXZ / time;
		return velocityXY + velocityY ; 
	}
	//détruitre le projectile
	void DestroyProjectile()
	{
		Destroy(gameObject);
	}

	/*
	 * détecter les collisions
	 * assigner les morts
	 * appeler la méthode de sélection du prochain joueur de la classe Game manager
	*/
	void OnCollisionEnter(Collision col)
	{
		DestroyProjectile();
		if (col.gameObject.tag == "Player")
		{
			GameObject player = col.gameObject;
			player.GetComponent<Launcher>().isDead = true;
			gameManager.GetComponent<GameManager>().playersAlive.Remove(player);
			string playersTag = gameManager.GetComponent<GameManager>().playersTag;
			gameManager.GetComponent<GameManager>().choosePlayerOfNextTurn(playersTag);
		}
		if (col.gameObject.tag == "Enemy")
		{
			GameObject enemy = col.gameObject;
			enemy.GetComponent<Launcher>().isDead = true;
			gameManager.GetComponent<GameManager>().enemiesAlive.Remove(enemy);
			string enemiesTag = gameManager.GetComponent<GameManager>().enemiesTag;
			gameManager.GetComponent<GameManager>().choosePlayerOfNextTurn(enemiesTag);
		}
	}


}

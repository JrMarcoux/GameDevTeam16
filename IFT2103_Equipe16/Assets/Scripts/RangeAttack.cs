using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : MonoBehaviour {
	private Rigidbody projectile;
	public string nameOfTargets;
	public float lifeTime = 5f;
	public List<GameObject> targets;
	private Transform target;
	public float verticalMaxDisplacement = 2f;
	[Range(-0.01f,-100)]
	public float gravity = -18f;
	private GameObject gameManager;
    public powerBarScript powerBar;

    void Start()
	{
        powerBar = GameObject.Find("powerBar").GetComponent<powerBarScript>();
        gameManager = GameObject.FindGameObjectWithTag("Game manager");
		Invoke("DestroyProjectile", lifeTime);
        projectile = gameObject.GetComponent<Rigidbody>();
		projectile.useGravity = false;
		if (nameOfTargets == "Player")
		{
			targets = gameManager.GetComponent<GameManager>().playersAlive;
		}
		if (nameOfTargets == "Enemy")
		{
			targets = gameManager.GetComponent<GameManager>().enemiesAlive;
		}
		int nbTargets = targets.Count;
		target = targets[Random.Range(0,nbTargets)].transform;
		Launch();
	}

	//lancer le projectile
	void Launch()
	{
		Physics.gravity = Vector3.up * gravity;
		projectile.useGravity = true;
		projectile.velocity = CalcLaunchVelocity();
		Invoke("DestroyProjectile", 5.0f);
	}
	//calculer la vitesse du lancer
	Vector3 CalcLaunchVelocity()
	{       

        float displacementY = target.position.y - projectile.position.y;

        Vector3 displacementXZ;
        if (0.40 < powerBar.GetAmount() && powerBar.GetAmount() < 0.60)
        {
            displacementXZ = new Vector3(target.position.x - projectile.position.x, 0, target.position.z - projectile.position.z);
        }
        else
        {
            displacementXZ = new Vector3(target.position.x * 2 * powerBar.GetAmount() - projectile.position.x, 0, target.position.z * 2 * powerBar.GetAmount() - projectile.position.z);
        }           
		Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * verticalMaxDisplacement);
		float time = Mathf.Sqrt(-2 * verticalMaxDisplacement / gravity) + Mathf.Sqrt(2 * (displacementY - verticalMaxDisplacement) / gravity);
		Vector3 velocityXY = displacementXZ / time;
		return velocityXY + velocityY ; 
	}
	//détruitre le projectile
	void DestroyProjectile()
	{
		Destroy(gameObject);
		if (nameOfTargets == "Enemy")
		{
			gameManager.GetComponent<GameManager>().choosePlayerOfNextTurn("Enemy");
		}
		if (nameOfTargets == "Player")
		{
			gameManager.GetComponent<GameManager>().choosePlayerOfNextTurn("Player");
		}
	}

	/*
	 * détecter les collisions
	 * assigner les morts
	 * appeler la méthode de sélection du prochain joueur de la classe Game manager
	*/
	void OnCollisionEnter(Collision col)
	{
        int deadHash = Animator.StringToHash("Dead");
		
		if (col.gameObject.tag == "Player")
		{
			GameObject player = col.gameObject;
            player.GetComponent<Animator>().SetTrigger(deadHash);
            player.GetComponent<Launcher>().isDead = true;          
            gameManager.GetComponent<GameManager>().playersAlive.Remove(player);
            powerBar.IncreaseSpeed();
			DestroyProjectile();

		}
		if (col.gameObject.tag == "Enemy")
		{
			GameObject enemy = col.gameObject;
            enemy.GetComponent<Animator>().SetTrigger(deadHash);
            enemy.GetComponent<Launcher>().isDead = true;           
			gameManager.GetComponent<GameManager>().enemiesAlive.Remove(enemy);
            powerBar.IncreaseSpeed();
            DestroyProjectile();

		}
		
	}


}

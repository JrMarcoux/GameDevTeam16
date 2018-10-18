using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : MonoBehaviour {
	private Rigidbody projectile;
	private BallisticPhysics physicScript;
	public string nameOfTargets;
	public float lifeTime = 5f;
	public List<GameObject> targets;
	private GameObject target;
	private GameObject gameManager;
  public powerBarScript powerBar;
	public GameObject outBoundaries;
	private bool checkAABB = false;
	private bool outsideAABB = false;
   private bool targetAABB = false;

    void Start()
	{
		physicScript = gameObject.GetComponent<BallisticPhysics>();
		outBoundaries = GameObject.Find("AABB");
		checkAABB = false;
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
		target = targets[Random.Range(0,nbTargets)];
		Launch();
	}
	void Update()
	{
		if (checkAABB)
		{
			outsideAABB = !outBoundaries.GetComponent<CollisionAABB>().CheckIfObjectIsInAABB(gameObject);
            targetAABB = target.GetComponent<CollisionAABB>().CheckIfObjectIsInAABB(gameObject);
		}
		if (outsideAABB)//si la balle dépasse les limites de la boite AABB, elle est détruite
		{
			DestroyProjectile();
		}
        if (targetAABB)
        {
            CollisionTarget();
        }
	}

	//lancer le projectile
	void Launch()
	{
		checkAABB = true; // on enclanche la détection pour regarder si la balle dépasse la boite AABB
		projectile.useGravity = true;
		float offset = powerBar.GetAmount(); //valeur de la powerbar
		physicScript.BallisticLaunch(projectile, target, offset); //lancer le projectile avec la physique
		Invoke("DestroyProjectile", 5.0f);
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
	void CollisionTarget()
	{
        int deadHash = Animator.StringToHash("Dead");

        target.GetComponent<Animator>().SetTrigger(deadHash);
        target.GetComponent<Launcher>().isDead = true;
        target.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 5);

        if (target.tag == "Player")
		{                    
            gameManager.GetComponent<GameManager>().playersAlive.Remove(target);       
		}
		if (target.tag == "Enemy")
		{      
			gameManager.GetComponent<GameManager>().enemiesAlive.Remove(target);
		}

        powerBar.IncreaseSpeed();
        DestroyProjectile();

    }


}

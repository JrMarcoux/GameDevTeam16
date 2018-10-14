using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : MonoBehaviour {
	private Rigidbody projectile;
	public string nameOfTargets;
	public float lifeTime = 5f;
	public List<GameObject> targets;
	private GameObject target;
	public float verticalMaxDisplacement = 2f;
	[Range(-0.01f,-100)]
	public float gravity = -18f;
	private GameObject gameManager;
  public powerBarScript powerBar;
	public GameObject outBoundaries;
	private bool checkAABB = false;
	private bool outsideAABB = false;
    private bool targetAABB = false;

    void Start()
	{
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
		if (outsideAABB)
		{
			DestroyProjectile();//si la balle dépasse les limites de la boite AABB, elle est détruite
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
		Physics.gravity = Vector3.up * gravity;
		projectile.useGravity = true;
		projectile.velocity = CalcLaunchVelocity();
		Invoke("DestroyProjectile", 5.0f);
	}
	//calculer la vitesse du lancer
	Vector3 CalcLaunchVelocity()
	{       

    float displacementY = target.transform.position.y - projectile.position.y;

    Vector3 displacementXZ;
		//valeur de la powerbar
    if (0.40 < powerBar.GetAmount() && powerBar.GetAmount() < 0.60)
    {
        displacementXZ = new Vector3(target.transform.position.x - projectile.position.x, 0, target.transform.position.z - projectile.position.z);
    }
    else
    {
        displacementXZ = new Vector3(target.transform.position.x * 2 * powerBar.GetAmount() - projectile.position.x, 0, target.transform.position.z * 2 * powerBar.GetAmount() - projectile.position.z);
    }
		
		//si la cible est plus haute que la hauteur max, ajuster la hauteur max
		if(target.transform.position.y > verticalMaxDisplacement)
		{
			verticalMaxDisplacement = target.transform.position.y + target.GetComponent<Collider>().bounds.size.y;
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
	void CollisionTarget()
	{
        int deadHash = Animator.StringToHash("Dead");

        target.GetComponent<Animator>().SetTrigger(deadHash);
        target.GetComponent<Launcher>().isDead = true;

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

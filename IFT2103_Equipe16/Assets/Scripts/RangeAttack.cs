using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : MonoBehaviour {
	private Rigidbody projectile;
	private BallisticPhysics physicScript;
	public float lifeTime = 5f;
	private GameObject target;
	private GameObject gameManager;
    private powerBarScript powerBar;
	private GameObject outBoundaries;
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
        if (gameManager.GetComponent<GameManager>().teamTurn == gameManager.GetComponent<GameManager>().playerTag)
        {
            target = gameManager.GetComponent<GameManager>().enemiesAlive[gameManager.GetComponent<GameManager>().selectedPlayerTarget];
        }
        else if (gameManager.GetComponent<GameManager>().teamTurn == gameManager.GetComponent<GameManager>().enemiesTag)
        {
            target = gameManager.GetComponent<GameManager>().playerAvatarsAlive[gameManager.GetComponent<GameManager>().selectedEnemyTarget];
        }
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
        gameManager.GetComponent<GameManager>().changeTurn();
        Destroy(gameObject);      
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

        if (target.tag == gameManager.GetComponent<GameManager>().playerTag)
		{
            gameManager.GetComponent<GameManager>().playerAvatarsAlive[gameManager.GetComponent<GameManager>().selectedPlayerAvatar].GetComponent<controlPlayer>().enabled = false;
            gameManager.GetComponent<GameManager>().playerAvatarsAlive.Remove(target);
        }
		if (target.tag == gameManager.GetComponent<GameManager>().enemiesTag)
		{
            gameManager.GetComponent<GameManager>().enemiesAlive[gameManager.GetComponent<GameManager>().selectedEnemyAvatar].GetComponent<IAEnemyScript>().enabled = false;
            gameManager.GetComponent<GameManager>().enemiesAlive.Remove(target);
        }       

        powerBar.IncreaseSpeed();
        DestroyProjectile();

    }


}

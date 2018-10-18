using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallisticPhysics : MonoBehaviour {

	
	public float verticalMaxDisplacement = 2f;
	[Range(-0.01f, -100)]
	public float gravity = -18f;

	//lancer le projectile avec la physique
	public void BallisticLaunch(Rigidbody projectile, GameObject target, float offsetLaunch = 0.50f)
	{
		Physics.gravity = Vector3.up * gravity;

		//calculer le déplacement
		float displacementY = target.transform.position.y - projectile.position.y;
		Vector3 displacementXZ;
		if (0.40 < offsetLaunch && offsetLaunch < 0.60){
			displacementXZ = new Vector3(target.transform.position.x - projectile.position.x, 0, target.transform.position.z - projectile.position.z);
		}
		else{
			displacementXZ = new Vector3(target.transform.position.x * 2 * offsetLaunch - projectile.position.x, 0, target.transform.position.z * 2 * offsetLaunch - projectile.position.z);
		}

		//ajuster la hauteur max si la cible est plus haute que la hauteur max, 
		if (target.transform.position.y > verticalMaxDisplacement){
			verticalMaxDisplacement = target.transform.position.y + target.GetComponent<Collider>().bounds.size.y;
		}
		//calcuer la vélocité
		Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * verticalMaxDisplacement);
		float time = Mathf.Sqrt(-2 * verticalMaxDisplacement / gravity) + Mathf.Sqrt(2 * (displacementY - verticalMaxDisplacement) / gravity);
		Vector3 velocityXY = displacementXZ / time;

		//lancer le projectile
		projectile.velocity = velocityXY + velocityY;
	}
}

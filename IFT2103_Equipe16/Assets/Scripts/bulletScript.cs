using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour {

	void OnCollisionEnter(Collision collision)
	{
		var objColl = collision.gameObject;
		var health = objColl.GetComponent<HealthMultiplayer>();
		if (health != null)
		{
			health.TakeDamage(10);
		}
		Destroy(gameObject);
	}
}

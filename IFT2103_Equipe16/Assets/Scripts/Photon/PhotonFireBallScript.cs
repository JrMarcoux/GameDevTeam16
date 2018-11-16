using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonFireBallScript : MonoBehaviour
{
	public float verticalMaxDisplacement = 2f;
	[Range(-0.01f, -100)]
	public float gravity = -18f;
	

	void Start()
	{
		
		Destroy(gameObject, 8.0f);
	}

	

	void OnCollisionEnter(Collision collision)
	{
		var objColl = collision.gameObject;
		var health = objColl.GetComponent<PlayerHealth>();
		if (health != null)
		{
			health.TakeDamage(10);
		}
		//Destroy(gameObject);
	}	
}
	


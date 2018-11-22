using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouvement : MonoBehaviour {

	private PhotonView PV;
	public float speed = 1f;
	public GameObject bulletObject;
	public Transform bulletSpawnLeft;
	public Transform bulletSpawnRight;
	public float velocityBulletSpeed = 6;
	//public GameObject networkManager;
	public bool allowFire = true;
	public float fireRate = 0.20f;
	public float xMinLimit = -100f;
	public float xMaxLimit = 100f; 
	public float zMinLimit = -100f;
	public float zMaxLimit = 100f;


	void Start()
	{
		//changer le sprite et l'animation pour le joueur local
		//GetComponent<SpriteRenderer>().sprite = spriteLocal;
		//GetComponent<Animator>().runtimeAnimatorController = animatorController;
		PV = GetComponent<PhotonView>();


	}

	void Update()
	{
		// ignorer les touches si le joueur n'est pas local
		if (PV.IsMine)
		{
			Mouvement();
		}
	}

	void Mouvement()
	{
		if (Input.GetKey(GetKeyPrefs("Down")))
		{
			if (transform.position.z > zMinLimit)
			{
				transform.Translate(0f, 0f, -speed * Time.deltaTime);

			}
		}
		if (Input.GetKey(GetKeyPrefs("Up")))
		{
			if (transform.position.z < zMaxLimit)
			{
				transform.Translate(0f, 0f, speed * Time.deltaTime);

			}
		}
		if (Input.GetKey(GetKeyPrefs("Right")))
		{
			if (transform.position.x < xMaxLimit)
			{
				transform.Translate(speed * Time.deltaTime, 0f, 0f);

			}
		}
		if (Input.GetKey(GetKeyPrefs("Left")))
		{
			if (transform.position.x > xMinLimit)
			{
				transform.Translate(-speed * Time.deltaTime, 0f, 0f);
			}
		}
		if ((Input.GetKeyDown(GetKeyPrefs("Fire"))) && (allowFire))
		{
			PV.RPC("RPC_Fire", RpcTarget.All);
			StartCoroutine(waitForFire());
			

		}

	}


	public KeyCode GetKeyPrefs(string keyName)
	{
		return (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(keyName));
	}
	
	[PunRPC]
	void RPC_Fire()
	{

		if (transform.position.x > 0)
		{
			var bullet = (GameObject)Instantiate(bulletObject, bulletSpawnLeft.position, bulletSpawnLeft.rotation);
			bullet.GetComponent<Rigidbody>().velocity = bullet.transform.right * -velocityBulletSpeed;
			Destroy(bullet, 2.0f);
		}
		else if (transform.position.x < 0)
		{
			var bullet = (GameObject)Instantiate(bulletObject, bulletSpawnRight.position, bulletSpawnRight.rotation);
			bullet.GetComponent<Rigidbody>().velocity = bullet.transform.right * velocityBulletSpeed;
			Destroy(bullet, 2.0f);
		}




	}
	IEnumerator waitForFire()
	{
		allowFire = false;
		yield return new WaitForSeconds(fireRate);
		allowFire = true;
	}


}

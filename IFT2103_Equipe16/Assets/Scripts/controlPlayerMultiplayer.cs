using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class controlPlayerMultiplayer : NetworkBehaviour
{

    public float speed = 1f;
		public Sprite spriteLocal;
	  public RuntimeAnimatorController animatorController;
		public GameObject bulletObject;
		public Transform bulletSpawnLeft;
		public Transform bulletSpawnRight;
		public float velocityBulletSpeed = 6;
	  public GameObject networkManager;
		

		public override void OnStartLocalPlayer()
		{
		//changer le sprite et l'animation pour le joueur local
		GetComponent<SpriteRenderer>().sprite = spriteLocal;
		GetComponent<Animator>().runtimeAnimatorController = animatorController;
		
	}

		void Update()
    {
		// ignorer les touches si le joueur n'est pas local
		if (!isLocalPlayer)
		{
			return;
		}
		else
		{
			if (Input.GetKey(GetKeyPrefs("Down")))
			{
				transform.Translate(0f, 0f, -speed * Time.deltaTime);
			}
			if (Input.GetKey(GetKeyPrefs("Up")))
			{
				transform.Translate(0f, 0f, speed * Time.deltaTime);
			}
			if (Input.GetKey(GetKeyPrefs("Right")))
			{
				transform.Translate(speed * Time.deltaTime, 0f, 0f);
			}
			if (Input.GetKey(GetKeyPrefs("Left")))
			{
				transform.Translate(-speed * Time.deltaTime, 0f, 0f);
			}
			if (Input.GetKeyDown(GetKeyPrefs("Fire")))
			{
				CmdFire();
			}
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				NetworkManager.Shutdown();
				Destroy(networkManager);
				SceneManager.LoadScene(0);
			}
		}
				
    }

    public KeyCode GetKeyPrefs(string keyName)
    {
        return (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(keyName));
    }
	[Command]
	void CmdFire()
	{

		if (transform.position.x > 0)
		{
		var bullet = (GameObject)Instantiate(bulletObject, bulletSpawnLeft.position, bulletSpawnLeft.rotation);
			bullet.GetComponent<Rigidbody>().velocity = bullet.transform.right * -velocityBulletSpeed;
			NetworkServer.Spawn(bullet);
			Destroy(bullet, 2.0f);
		}
		else if (transform.position.x < 0)
		{
			var bullet = (GameObject)Instantiate(bulletObject, bulletSpawnRight.position, bulletSpawnRight.rotation);
			bullet.GetComponent<Rigidbody>().velocity = bullet.transform.right * velocityBulletSpeed;
			NetworkServer.Spawn(bullet);
			Destroy(bullet, 2.0f);
		}

		
	}
}

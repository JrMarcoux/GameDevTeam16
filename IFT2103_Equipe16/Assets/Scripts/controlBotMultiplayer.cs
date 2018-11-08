using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class controlBotMultiplayer : NetworkBehaviour
{

	public Sprite spriteLocal;
	public RuntimeAnimatorController animatorController;
	public GameObject bulletObject;
	public Transform bulletSpawn;
	public float minX = 0;
	public float maxX = 0;
	public float minZ = 0;
	public float maxZ = 0;

	public void changeSprite()
	{
		//changer le sprite et l'animation
		GetComponent<SpriteRenderer>().sprite = spriteLocal;
		GetComponent<Animator>().runtimeAnimatorController = animatorController;
	}
	

	[Command]
	public void CmdFire()
	{
		var bullet = (GameObject)Instantiate(bulletObject, bulletSpawn.position, bulletSpawn.rotation);
		gameObject.GetComponent<BallisticPhysics>().BallisticLaunchMultiplayer(bullet.GetComponent<Rigidbody>(), minX, maxX, minZ, maxZ);
		NetworkServer.Spawn(bullet);
		Destroy(bullet, 8.0f);

		StartCoroutine(waitAndFire(Random.Range(1f, 3f)));

	}

	IEnumerator waitAndFire(float time)
	{
		yield return new WaitForSeconds(time);
		CmdFire();
	}



}
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class PhotonControlBot : MonoBehaviour
{

	public Sprite spriteLocal;
	public RuntimeAnimatorController animatorController;
	public GameObject bulletObject;
	public Transform bulletSpawn;
	public float minX = 0;
	public float maxX = 0;
	public float minZ = 0;
	public float maxZ = 0;
	private PhotonView PV;
	public float verticalMaxDisplacement = 2f;
	[Range(-0.01f, -100)]
	public float gravity = -18f;

	public void changeSprite()
	{
		//changer le sprite et l'animation
		GetComponent<SpriteRenderer>().sprite = spriteLocal;
		GetComponent<Animator>().runtimeAnimatorController = animatorController;
	}
	void Start()
	{
		PV = GetComponent<PhotonView>();
		if (PhotonNetwork.IsMasterClient)
		{
			PV.RPC("RPC_Fire", RpcTarget.All);
		}
		
	}
	

	[PunRPC]
	public void RPC_Fire()
	{
		
		if (PhotonNetwork.IsMasterClient)
		{
			var bullet = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "PhotonFireBall"), bulletSpawn.position, bulletSpawn.rotation);
			//var bullet = (GameObject)Instantiate(bulletObject, bulletSpawn.position, bulletSpawn.rotation);

			Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
			//PV.RPC("RPC_BalisticPhysics", RpcTarget.All, minX, maxX, minZ, maxZ);
			Vector3 velocity = balisticPhysics(bulletRigidbody, minX, maxX, minZ, maxZ);

			bullet.GetComponent<Rigidbody>().velocity = velocity;

			
		}
		//gameObject.GetComponent<BallisticPhysics>().BallisticLaunchMultiplayer(bullet.GetComponent<Rigidbody>(), minX, maxX, minZ, maxZ);
		

		StartCoroutine(waitAndFire(Random.Range(1f, 3f)));

	}

	IEnumerator waitAndFire(float time)
	{
		yield return new WaitForSeconds(time);
		if (PhotonNetwork.IsMasterClient)
		{
			PV.RPC("RPC_Fire", RpcTarget.All);
		}
	}


	//lancer le projectile avec la physique version multi

	public Vector3 balisticPhysics(Rigidbody bulletRigidbody,float xMin, float xMax, float zMin, float zMax)
	{
		Physics.gravity = Vector3.up * gravity;

		//calculer le déplacement

		Vector3 displacementXZ;
		displacementXZ = new Vector3(Random.Range(xMin, xMax) - bulletRigidbody.position.x, 0, Random.Range(zMin, zMax) - bulletRigidbody.position.z);

		//ajuster la hauteur max si la cible est plus haute que la hauteur max, 

		//calcuer la vélocité
		Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * verticalMaxDisplacement);
		float time = Mathf.Sqrt(-2 * verticalMaxDisplacement / gravity) + Mathf.Sqrt(2 * (0 - verticalMaxDisplacement) / gravity);
		Vector3 velocityXY = displacementXZ / time;

		//lancer le projectile
		return velocityXY + velocityY;

	}



}
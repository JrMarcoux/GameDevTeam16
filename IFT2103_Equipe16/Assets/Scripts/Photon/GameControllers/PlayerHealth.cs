using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

	public const int maxHealth = 100;
	//[SyncVar(hook = "changeHealth")]
	public int currentHealth;
	public RectTransform healthBar;
	private Vector3 spawnPosition;
	private PhotonView PV;
	public GameObject myCharacter;






	void Start()
	{

		PV = GetComponent<PhotonView>();
		if (PV.IsMine)
		{

			myCharacter = gameObject;
		}

		/*if (isLocalPlayer)
		{
			int nbPLayer = GameObject.FindGameObjectsWithTag("Player").Length;
			if (nbPLayer > 1)
			{
				gameObject.tag = "Player2";
				spawnPosition = GameObject.FindGameObjectWithTag("SpawnPlayer2").transform.position;
				gameObject.GetComponent<controlPlayerMultiplayer>().xMinLimit = GameObject.FindGameObjectWithTag("SpawnPlayer2").GetComponent<spawnPositionMulti>().xMinLimit;
				gameObject.GetComponent<controlPlayerMultiplayer>().xMaxLimit = GameObject.FindGameObjectWithTag("SpawnPlayer2").GetComponent<spawnPositionMulti>().xMaxLimit;
				gameObject.GetComponent<controlPlayerMultiplayer>().zMinLimit = GameObject.FindGameObjectWithTag("SpawnPlayer2").GetComponent<spawnPositionMulti>().zMinLimit;
				gameObject.GetComponent<controlPlayerMultiplayer>().zMaxLimit = GameObject.FindGameObjectWithTag("SpawnPlayer2").GetComponent<spawnPositionMulti>().zMaxLimit;
				CmdstartBotFire();

			}
			else
			{
				gameObject.tag = "Player1";
				spawnPosition = GameObject.FindGameObjectWithTag("SpawnPlayer1").transform.position;
				gameObject.GetComponent<controlPlayerMultiplayer>().xMinLimit = GameObject.FindGameObjectWithTag("SpawnPlayer1").GetComponent<spawnPositionMulti>().xMinLimit;
				gameObject.GetComponent<controlPlayerMultiplayer>().xMaxLimit = GameObject.FindGameObjectWithTag("SpawnPlayer1").GetComponent<spawnPositionMulti>().xMaxLimit;
				gameObject.GetComponent<controlPlayerMultiplayer>().zMinLimit = GameObject.FindGameObjectWithTag("SpawnPlayer1").GetComponent<spawnPositionMulti>().zMinLimit;
				gameObject.GetComponent<controlPlayerMultiplayer>().zMaxLimit = GameObject.FindGameObjectWithTag("SpawnPlayer1").GetComponent<spawnPositionMulti>().zMaxLimit;


			}
			transform.position = spawnPosition;
		}*/
	}

	void Update()
	{
		
		
		/*
		//le joueur tombe en bas de la plateforme
		if (transform.position.y < -10)
		{
			currentHealth = maxHealth;
			RpcRespawn();
		}

		*/
	}
	public void TakeDamage(int dmg)
	{
		if (PV.IsMine)
		{
			PV.RPC("RPC_TakeDamage", RpcTarget.AllBuffered, dmg);
		}

		/*
		if (!isServer) // c'est seulement le serveur qui applique les dommages
		{
			return;
		}
		currentHealth -= dmg;
		if (currentHealth <= 0)
		{
			currentHealth = maxHealth;
			RpcRespawn();
		}*/
	}

	[PunRPC]
	void RPC_TakeDamage(int dmg)
	{
		currentHealth -= dmg;
		if (currentHealth <= 0)
		{
			currentHealth = maxHealth;
			
			//RpcRespawn();
		}
		healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);
	}
	/*
	//changer la barre de vie en fonction de la vie 
	void changeHealth(int health)
	{
		healthBar.sizeDelta = new Vector2(health, healthBar.sizeDelta.y);
	}
	*/

		/*
	[Command]
	void CmdstartBotFire()
	{
		GameObject.FindGameObjectWithTag("BotP1").GetComponent<controlBotMultiplayer>().CmdFire();
		GameObject.FindGameObjectWithTag("BotP2").GetComponent<controlBotMultiplayer>().CmdFire();
	}

	*/
	/*
	//Respawn du joueur local
	[ClientRpc]
	void RpcRespawn()
	{
		if (isLocalPlayer)
		{

			transform.position = spawnPosition;
		}
	}
	*/

}

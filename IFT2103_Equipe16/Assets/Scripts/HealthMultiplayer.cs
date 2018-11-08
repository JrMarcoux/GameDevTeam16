using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class HealthMultiplayer : NetworkBehaviour
{

	public const int maxHealth = 100;
	[SyncVar(hook = "changeHealth")]
	public int currentHealth;
	public RectTransform healthBar;
	private Vector3 spawnPosition;


	void Start()
	{
		if (isLocalPlayer)
		{
			int nbPLayer = GameObject.FindGameObjectsWithTag("Player").Length;
			if (nbPLayer>1)
			{
				gameObject.tag = "Player2";
				spawnPosition = GameObject.FindGameObjectWithTag("SpawnPlayer2").transform.position;
				
			}else
			{
				gameObject.tag = "Player1";
				spawnPosition = GameObject.FindGameObjectWithTag("SpawnPlayer1").transform.position;
			}
			transform.position = spawnPosition;
		}
	}

	void Update()
	{
		if (transform.position.y < -10)
		{
			currentHealth = maxHealth;
			RpcRespawn();
		}
	}
	public void TakeDamage(int dmg)
	{
		// c'est seulement le serveur qui applique les dommages
		if (!isServer)
		{
			return;
		}
		currentHealth -= dmg;
		if (currentHealth <= 0)
		{
			currentHealth = maxHealth;
			RpcRespawn();
		}
		
		
	}
	void changeHealth(int health)
	{
		healthBar.sizeDelta = new Vector2(health, healthBar.sizeDelta.y);
	}
	[ClientRpc]
	void RpcRespawn()
	{
		if(isLocalPlayer)
		{

			transform.position = spawnPosition;
		}
	}

}

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
	private GameObject GS;
	private bool IsDead = false;
	private bool stopUpdate = false;



	void Start()
	{
		GS = GameObject.FindGameObjectWithTag("GameSetup");
		PV = GetComponent<PhotonView>();
		if (PV.IsMine)
		{

			myCharacter = gameObject;
		}

	}

	void Update()
	{
		if (!stopUpdate)
		{
			if (IsDead)
			{
				PV.RPC("RPC_FinishGame", RpcTarget.AllBuffered);
			}
			if (GS.GetComponent<GameSetup>().GameIsFinish)
			{
				stopUpdate = true;
				if (PV.IsMine)
				{
					GameObject.FindGameObjectWithTag("MainCamera").GetComponent<EndGameCamera>().target = gameObject.transform;
					GameObject.FindGameObjectWithTag("MainCamera").GetComponent<EndGameCamera>().fallow = true;
					if (IsDead)
					{
						GS.GetComponent<GameSetup>().playerLoser = true;
					}
					GS.GetComponent<GameSetup>().activeEndGameButton();
				}
			}
		}
		
	}
	public void TakeDamage(int dmg)
	{
		if (!GS.GetComponent<GameSetup>().GameIsFinish)
		{
			if (PV.IsMine)
			{
				PV.RPC("RPC_TakeDamage", RpcTarget.AllBuffered, dmg);
			}
		}
		
	}

	[PunRPC]
	void RPC_FinishGame()
	{
		GS.GetComponent<GameSetup>().GameIsFinish = true;
	}

	[PunRPC]
	void RPC_TakeDamage(int dmg)
	{
		currentHealth -= dmg;
		if (currentHealth <= 0)
		{
			//currentHealth = maxHealth;
			IsDead = true;
			

		}
		healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);
	}


}

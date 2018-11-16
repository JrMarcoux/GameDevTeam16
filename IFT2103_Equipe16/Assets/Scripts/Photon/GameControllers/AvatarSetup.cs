using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarSetup : MonoBehaviour {

	private PhotonView PV;
	public GameObject myCharacter;
	public Sprite spriteLocal;
	public RuntimeAnimatorController animatorController;
	public Vector3 spawnPosition;
	


	void Start() {
		PV = GetComponent<PhotonView>();
		if (PV.IsMine)
		{
			GetComponent<SpriteRenderer>().sprite = spriteLocal;
			GetComponent<Animator>().runtimeAnimatorController = animatorController;
			myCharacter = gameObject;
			if (gameObject.tag == "Player1")
			{
				PV.RPC("RPC_setTag", RpcTarget.All, "Player1");
				GameObject.FindGameObjectWithTag("BotP2").GetComponent<PhotonControlBot>().changeSprite();
			}
			else if(gameObject.tag == "Player2")
			{
				PV.RPC("RPC_setTag", RpcTarget.All, "Player2");
				GameObject.FindGameObjectWithTag("BotP1").GetComponent<PhotonControlBot>().changeSprite();
			}
		}
			
		
	}

	[PunRPC]
	void RPC_setTag(string tag)
	{
		gameObject.tag = tag;
	}
	
}

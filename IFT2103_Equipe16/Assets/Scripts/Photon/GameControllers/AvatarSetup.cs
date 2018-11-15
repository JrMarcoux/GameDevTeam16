﻿using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarSetup : MonoBehaviour {

	private PhotonView PV;
	public GameObject myCharacter;
	public Sprite spriteLocal;
	public RuntimeAnimatorController animatorController;


	void Start() {
		PV = GetComponent<PhotonView>();
		if (PV.IsMine)
		{
			GetComponent<SpriteRenderer>().sprite = spriteLocal;
			GetComponent<Animator>().runtimeAnimatorController = animatorController;
			myCharacter = gameObject;
		}
		
	}
	
}
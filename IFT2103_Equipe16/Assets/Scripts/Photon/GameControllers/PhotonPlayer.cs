using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PhotonPlayer : MonoBehaviour {

	private PhotonView PV;
	public GameObject myAvatar;

	// Use this for initialization
	void Start() {
		PV = GetComponent<PhotonView>();
		
		if (PhotonNetwork.IsMasterClient)
		{
			if (PV.IsMine)
			{
				myAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerAvatar"),
					GameSetup.GS.spawnPoints[0].position, GameSetup.GS.spawnPoints[0].rotation, 0);
				myAvatar.tag = "Player1";
				myAvatar.GetComponent<PlayerMouvement>().xMaxLimit = -0.5f;
				myAvatar.GetComponent<PlayerMouvement>().xMinLimit = -4f;
				myAvatar.GetComponent<PlayerMouvement>().zMaxLimit = 3.5f;
				myAvatar.GetComponent<PlayerMouvement>().zMinLimit = -3.5f;
				PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "PhotonBotP1"), GameSetup.GS.spawnBotP1.transform.position, GameSetup.GS.spawnBotP1.transform.rotation, 0);
				PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "PhotonBotP2"), GameSetup.GS.spawnBotP2.transform.position, GameSetup.GS.spawnBotP2.transform.rotation, 0);
			}
		}else
		{
			if (PV.IsMine)
			{
				myAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerAvatar"),
					GameSetup.GS.spawnPoints[1].position, GameSetup.GS.spawnPoints[1].rotation, 0);
				myAvatar.tag = "Player2";
				myAvatar.GetComponent<PlayerMouvement>().xMaxLimit = 4f;
				myAvatar.GetComponent<PlayerMouvement>().xMinLimit = 0.5f;
				myAvatar.GetComponent<PlayerMouvement>().zMaxLimit = 3.5f;
				myAvatar.GetComponent<PlayerMouvement>().zMinLimit = -3.5f;
			}
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	[PunRPC]
	private void RPC_Bots()
	{
		PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonBotP1"), GameSetup.GS.spawnBotP1.transform.position, GameSetup.GS.spawnBotP1.transform.rotation, 0);
		
	}
}

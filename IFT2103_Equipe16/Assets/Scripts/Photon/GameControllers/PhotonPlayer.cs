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
		int spawnPicker = Random.Range(0, GameSetup.GS.spawnPoints.Length);
		if (PhotonNetwork.IsMasterClient)
		{
			if (PV.IsMine)
			{
				myAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerAvatar"),
					GameSetup.GS.spawnPoints[0].position, GameSetup.GS.spawnPoints[0].rotation, 0);
			}
		}else
		{
			if (PV.IsMine)
			{
				myAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerAvatar"),
					GameSetup.GS.spawnPoints[1].position, GameSetup.GS.spawnPoints[1].rotation, 0);
			}
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PhotonLobby : MonoBehaviourPunCallbacks {

	public static PhotonLobby lobby;
	public GameObject battleButton;
	public GameObject cancelButton;

	private void Awake()
	{
		lobby = this; // créer un singleton
	}

	void Start () {
		PhotonNetwork.ConnectUsingSettings(); //pour se conecter au master serveur photon
		
	}
	public override void OnConnectedToMaster() // à la connection au serveur
	{
		Debug.Log("connect to photon master server");
		battleButton.SetActive(true); // rendre le bouton actif pour joindre une partie
	}

	public void OnBattleButtonClicked()
	{
		Debug.Log("click battle button");
		battleButton.SetActive(false);
		cancelButton.SetActive(true);
		PhotonNetwork.JoinRandomRoom(); 
	}
	public override void OnJoinRandomFailed(short returnCode, string message)
	{
		Debug.Log("try to join a random room but failed");
		CreateRoom();
		
	}
	void CreateRoom()
	{
		Debug.Log("try to create a new room");
		int randomRoomName = Random.Range(0, 10000);
		RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 2 };
		PhotonNetwork.CreateRoom("Room" + randomRoomName, roomOps);

	}
	public override void OnJoinedRoom()
	{
		Debug.Log("We are in a room");
	}

	public override void OnCreateRoomFailed(short returnCode, string message)
	{
		Debug.Log("try to create a new room but failed, it must be a room with the same name");
		CreateRoom();
	}

	public void OnCancelButtonClicked()
	{
		Debug.Log("click cancel button");
		cancelButton.SetActive(false);
		battleButton.SetActive(true);
		PhotonNetwork.LeaveRoom();
	}

	void Update () {
		
	}
}

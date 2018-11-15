using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhotonRoom : MonoBehaviourPunCallbacks, IInRoomCallbacks {

	public static PhotonRoom room;
	private PhotonView PV;
	public bool isGameLoaded;
	public int currentScene;

	Player[] photonPlayers;
	public int playerInRoom;
	public int myNumberInRoom;
	public int playerInGame;

	private bool readyToStart;
	private bool readyToCount;
	public float startingTime;
	private float lessThanMaxPlayers;
	private float atMaxPlayer;
	private float timeToStart;

	private void Awake()
	{
		if(PhotonRoom.room == null)
		{
			PhotonRoom.room = this;
		}
		else
		{
			if(PhotonRoom.room != this)
			{
				Destroy(PhotonRoom.room.gameObject);
				PhotonRoom.room = this;
			}
		}
		DontDestroyOnLoad(this.gameObject);
	}

	public override void OnEnable()
	{
		base.OnEnable();
		PhotonNetwork.AddCallbackTarget(this);
		SceneManager.sceneLoaded += OnSceneFinishedLoading;
	}

	public override void OnDisable()
	{
		base.OnDisable();
		PhotonNetwork.RemoveCallbackTarget(this);
		SceneManager.sceneLoaded -= OnSceneFinishedLoading;
	}


	void Start () {
		PV=GetComponent<PhotonView>();
		readyToCount = false;
		readyToStart = false;
		lessThanMaxPlayers = startingTime;
		atMaxPlayer = 0;
		timeToStart = startingTime;
	}
	
	// Update is called once per frame
	void Update () {
		if (MultiplayerSetting.multiplayerSetting.delayStart)
		{
			if(playerInRoom == 1)
			{
				RestartTimer();
			}
			if (!isGameLoaded)
			{
				if (readyToStart)
				{
					atMaxPlayer -= Time.deltaTime;
					lessThanMaxPlayers = atMaxPlayer;
					timeToStart = atMaxPlayer;
				}
				else if (readyToCount)
				{
					lessThanMaxPlayers -= Time.deltaTime;
					timeToStart = lessThanMaxPlayers;
				}
				Debug.Log("Time to start: " + timeToStart);
				if (timeToStart <= 0)
				{
					StartGame();
				}
			}
		}
	}

	public override void OnJoinedRoom()
	{
		base.OnJoinedRoom();
		Debug.Log("We are in a room");
		photonPlayers = PhotonNetwork.PlayerList;
		playerInRoom = photonPlayers.Length;
		myNumberInRoom = playerInRoom;
		PhotonNetwork.NickName = myNumberInRoom.ToString();
		if (MultiplayerSetting.multiplayerSetting.delayStart)
		{
			Debug.Log("Display player in room out of max playeer possible( " + playerInRoom + " : " + MultiplayerSetting.multiplayerSetting.maxPlayer + ")");
			if (playerInRoom > 1)
			{
				readyToCount = true;
			}
			if (playerInRoom == MultiplayerSetting.multiplayerSetting.maxPlayer)
			{
				readyToStart = true;
				if (!PhotonNetwork.IsMasterClient)
				{
					return;
				}
				PhotonNetwork.CurrentRoom.IsOpen = false;
			}
		}
		else
		{
			StartGame();
		}

	}

		public override void OnPlayerEnteredRoom(Player newPlayer)
	{
		base.OnPlayerEnteredRoom(newPlayer);
		Debug.Log("New player has joined the room");
		photonPlayers = PhotonNetwork.PlayerList;
		playerInRoom++;
		if (MultiplayerSetting.multiplayerSetting.delayStart)
		{
			Debug.Log("Display player in room out of max player possible( " + playerInRoom + " : " + MultiplayerSetting.multiplayerSetting.maxPlayer + ")");
			if (playerInRoom > 1)
			{
				readyToCount = true;
			}
			if (playerInRoom == MultiplayerSetting.multiplayerSetting.maxPlayer)
			{
				readyToStart = true;
				if (!PhotonNetwork.IsMasterClient)
				{
					return;
				}
				PhotonNetwork.CurrentRoom.IsOpen = false;
			}
		}

	}
	void StartGame()
	{
		isGameLoaded = true;
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		if (MultiplayerSetting.multiplayerSetting.delayStart)
		{
			PhotonNetwork.CurrentRoom.IsOpen = false;
		}
		PhotonNetwork.LoadLevel(MultiplayerSetting.multiplayerSetting.multiplayerScene);
	}

	void RestartTimer()
	{
		lessThanMaxPlayers = startingTime;
		timeToStart = startingTime;
		atMaxPlayer = 0;
		readyToCount = false;
		readyToStart = false;

	}

	void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
	{
		currentScene = scene.buildIndex;
		if(currentScene == MultiplayerSetting.multiplayerSetting.multiplayerScene)
		{
			isGameLoaded = true;
			if (MultiplayerSetting.multiplayerSetting.delayStart)
			{
				PV.RPC("RPC_LoadedGameScene", RpcTarget.MasterClient);
			}
			else
			{
				RPC_CreatePlayer();
			}
		}	
	}

	[PunRPC]
	private void RPC_LoadedGameScene()
	{
		playerInGame++;
		if (playerInGame == PhotonNetwork.PlayerList.Length)
		{
			PV.RPC("RPC_CreatePlayer", RpcTarget.All);
		}
	}

	[PunRPC]
	private void RPC_CreatePlayer()
	{
		PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonNetworkPlayer"), transform.position, Quaternion.identity,0);
	}



}

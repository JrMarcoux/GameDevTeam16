using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSetup : MonoBehaviour {

	public static GameSetup GS;

	public Transform[] spawnPoints;

	public GameObject spawnBotP1;
	public GameObject spawnBotP2;

	public bool GameIsFinish = false;

	public GameObject MainMenuBtn;
	public GameObject OnlineMenuBtn;
	public GameObject VictoryTxt;
	public GameObject DeafeatTxt;
	public GameObject HasLeftTxt;
	public GameObject GameMenu;
	public bool playerLoser = false;
	public bool playerHasLeft = false;

	private void OnEnable()
	{
		if(GameSetup.GS == null)
		{
			GameSetup.GS = this;
		}
	}

	public void DisconnectPLayer()
	{
		StartCoroutine(Disconect());
	}

	IEnumerator Disconect()
	{
		PhotonNetwork.Disconnect();
		while(PhotonNetwork.IsConnected)
			yield return null;
		SceneManager.LoadScene(4);
	}
	public void ReturnMainMenu()
	{
		StartCoroutine(DisconectMainMenu());
	}
	IEnumerator DisconectMainMenu()
	{
		PhotonNetwork.Disconnect();
		while (PhotonNetwork.IsConnected)
			yield return null;
		SceneManager.LoadScene(1);
	}

	public void EnabledMenu()
	{
		GameMenu.SetActive(true);
	}
	public void DisabledMenu()
	{
		GameMenu.SetActive(false);
	}
	public void activeEndGameButton()
	{
		MainMenuBtn.SetActive(true);
		OnlineMenuBtn.SetActive(true);
		if (playerLoser)
		{
			DeafeatTxt.SetActive(true);
		}
		else if (playerHasLeft)
		{
			HasLeftTxt.SetActive(true);
		}
		else
		{
			VictoryTxt.SetActive(true);
		}
	}
}

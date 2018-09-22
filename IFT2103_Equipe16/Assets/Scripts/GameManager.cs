using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public List<GameObject> playersAlive;
	public List<GameObject> enemiesAlive;
	private GameObject[] players;
	private GameObject[] enemies;
	public string playersTag ="Player";
	public string enemiesTag = "Enemy";


	void Start () {
		players = GameObject.FindGameObjectsWithTag(playersTag);
		enemies = GameObject.FindGameObjectsWithTag(enemiesTag); 
		foreach (GameObject player in players)
		{
			playersAlive.Add(player);
		}
		foreach (GameObject enemy in enemies)
		{
			enemiesAlive.Add(enemy);
		}
		//commencer la partie
		NextTurn(playersAlive[0]);
	}
	
	//choisir le prochain joueur à jouer
	public void choosePlayerOfNextTurn(string tagName)
	{
		if(tagName == playersTag)
		{
			if (playersAlive.Count > 0)
			{
				NextTurn(playersAlive[Random.Range(0, playersAlive.Count)]);
			}
		}
		else if(tagName == enemiesTag)
		{
			if (enemiesAlive.Count > 0)
			{
				NextTurn(enemiesAlive[Random.Range(0, enemiesAlive.Count)]);
			}
		}
	}

	//enclancher le prochain tour
	void NextTurn(GameObject playerToPlay)
	{
		StartCoroutine(playerToPlay.GetComponent<Launcher>().launch());
	}

	
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))//quitter la partie avec Escape
		{
			Application.Quit();
		}
	}

}

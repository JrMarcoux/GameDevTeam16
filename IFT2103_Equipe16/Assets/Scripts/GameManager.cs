using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	public List<GameObject> playersAlive;
	public List<GameObject> enemiesAlive;
	private GameObject[] players;
	private GameObject[] enemies;
    private List<GameObject> allCharacters;
    public string playersTag ="Player";
	public string enemiesTag = "Enemy";
    private GameObject redSprite;
    private GameObject blueSprite;
    private GameObject victory;
    public float chrono = 4;
    private bool chronoActive = false;


    void Start () {
		players = GameObject.FindGameObjectsWithTag(playersTag);
		enemies = GameObject.FindGameObjectsWithTag(enemiesTag);
        redSprite = GameObject.Find("powerBarRed");
        blueSprite = GameObject.Find("powerBarBlue");
        victory = GameObject.Find("victoryText");

        foreach (GameObject player in players)
		{
			playersAlive.Add(player);
		}
		foreach (GameObject enemy in enemies)
		{
			enemiesAlive.Add(enemy);
		}
        //commencer la partie
        redSprite.SetActive(false);
        blueSprite.SetActive(true);
        victory.SetActive(false);
        NextTurn(playersAlive[0]);
	}
	
	//choisir le prochain joueur à jouer
	public void choosePlayerOfNextTurn(string tagName)
	{
		if(tagName == playersTag)
		{
			if (playersAlive.Count > 0)
			{
                redSprite.SetActive(false);
                blueSprite.SetActive(true);
                NextTurn(playersAlive[Random.Range(0, playersAlive.Count)]);
			}
            else
            {
                victory.SetActive(true);
                GameObject.FindGameObjectWithTag("RedFountain").GetComponent<ParticleSystem>().Play();
                chronoActive = true;
            }
        }
		else if(tagName == enemiesTag)
		{
			if (enemiesAlive.Count > 0)
			{
                redSprite.SetActive(true);
                blueSprite.SetActive(false);
                NextTurn(enemiesAlive[Random.Range(0, enemiesAlive.Count)]);
			}
            else
            {
                victory.SetActive(true);
                GameObject.FindGameObjectWithTag("BlueFountain").GetComponent<ParticleSystem>().Play();
                chronoActive = true;
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
        if (chronoActive)
        {
            chrono -= Time.deltaTime;
        }
        if(chrono <= 0)
        {
            SceneManager.LoadScene(0);
        }
	}

}

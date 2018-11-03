using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	public List<GameObject> playerAvatarsAlive;
	public List<GameObject> enemiesAlive;
	private GameObject[] players;
	private GameObject[] enemies;
    
    public int selectedPlayerAvatar;
    public int selectedEnemyAvatar;
    public int selectedPlayerTarget;
    public int selectedEnemyTarget;
    public string teamTurn;

    public string playerTag ="Player";
	public string enemiesTag = "Enemy";


    private GameObject redSprite;
    private GameObject blueSprite;
    private GameObject victory;
    private GameObject[] particles;
    public float chrono = 4;
    private bool chronoActive = false;


    void Start () {
		players = GameObject.FindGameObjectsWithTag(playerTag);
		enemies = GameObject.FindGameObjectsWithTag(enemiesTag);
        redSprite = GameObject.Find("powerBarRed");
        blueSprite = GameObject.Find("powerBarBlue");
        victory = GameObject.Find("victoryText");

        foreach (GameObject player in players)
		{
            playerAvatarsAlive.Add(player);
		}
		foreach (GameObject enemy in enemies)
		{
			enemiesAlive.Add(enemy);
		}
        //commencer la partie
        redSprite.SetActive(false);
        blueSprite.SetActive(true);
        victory.SetActive(false);
        teamTurn = playerTag;
        selectedPlayerAvatar = 0;
        selectedEnemyAvatar = 0;
        selectedPlayerTarget = 0;
        selectedEnemyTarget = 0;
        playerAvatarsAlive[selectedPlayerAvatar].GetComponent<controlPlayer>().enabled = true;
        enemiesAlive[selectedEnemyAvatar].GetComponent<IAEnemyScript>().enabled = true;
        StartCoroutine(playerAvatarsAlive[selectedPlayerAvatar].GetComponent<Launcher>().launch());
    }
	
	//choisir le prochain joueur à jouer
	public void changeTurn()
	{
		if(teamTurn == enemiesTag)
		{
			if (playerAvatarsAlive.Count > 0)
			{
                redSprite.SetActive(false);
                blueSprite.SetActive(true);
                selectedPlayerAvatar = 0;
                selectedEnemyTarget = 0;
                changeSelectCharacter(playerTag);
                changeSelectTarget(enemiesTag);
                teamTurn = playerTag;                
                StartCoroutine(playerAvatarsAlive[selectedPlayerAvatar].GetComponent<Launcher>().launch());
            }
            else
            {
                victory.SetActive(true);
                particles = GameObject.FindGameObjectsWithTag("RedFountain");
                foreach (GameObject emitter in particles)
                {
                    emitter.GetComponent<ParticleSystem>().Play();
                }
                chronoActive = true;
            }
        }
		else if(teamTurn == playerTag)
		{
			if (enemiesAlive.Count > 0)
			{
                redSprite.SetActive(true);
                blueSprite.SetActive(false);
                selectedEnemyAvatar = 0;
                selectedPlayerTarget = 0;
                changeSelectCharacter(enemiesTag);
                changeSelectTarget(playerTag);
                teamTurn = enemiesTag;               
                StartCoroutine(enemiesAlive[selectedEnemyAvatar].GetComponent<Launcher>().launch());               
            }
            else
            {
                victory.SetActive(true);
                particles = GameObject.FindGameObjectsWithTag("BlueFountain");
                foreach (GameObject emitter in particles)
                {
                    emitter.GetComponent<ParticleSystem>().Play();
                }
                chronoActive = true;
            }
        }       
    }

	
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))//quitter la partie avec Escape
		{
            SceneManager.LoadScene(0);
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

    public void changeSelectCharacter(string team)
    {
        if(team == playerTag)
        {
            if(teamTurn == playerTag)
            {
                StopCoroutine(playerAvatarsAlive[selectedPlayerAvatar].GetComponent<Launcher>().launch());
            }            
            playerAvatarsAlive[selectedPlayerAvatar].GetComponent<controlPlayer>().enabled = false;
            selectedPlayerAvatar = (selectedPlayerAvatar + 1) % playerAvatarsAlive.Count;
            playerAvatarsAlive[selectedPlayerAvatar].GetComponent<controlPlayer>().enabled = true;
            if (teamTurn == playerTag)
            {
                StartCoroutine(playerAvatarsAlive[selectedPlayerAvatar].GetComponent<Launcher>().launch());
            }           
        }
        if (team == enemiesTag)
        {
            if(teamTurn == enemiesTag){
                StopCoroutine(enemiesAlive[selectedEnemyAvatar].GetComponent<Launcher>().launch());
            }            
            enemiesAlive[selectedEnemyAvatar].GetComponent<IAEnemyScript>().enabled = false;
            selectedEnemyAvatar = (selectedEnemyAvatar + 1) % enemiesAlive.Count;
            enemiesAlive[selectedEnemyAvatar].GetComponent<IAEnemyScript>().enabled = true;
            if (teamTurn == enemiesTag)
            {
                StartCoroutine(enemiesAlive[selectedEnemyAvatar].GetComponent<Launcher>().launch());
            }           
        }
    }

    public void changeSelectTarget(string team)
    {
        if (team == playerTag)
        {
            selectedPlayerTarget = (selectedPlayerTarget + 1) % enemiesAlive.Count;
        }
        if (team == enemiesTag)
        {
            selectedEnemyTarget = (selectedEnemyTarget + 1) % playerAvatarsAlive.Count;
        }
    }
}

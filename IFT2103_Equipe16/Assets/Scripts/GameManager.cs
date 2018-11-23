using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<GameObject> playerAvatarsAlive;
    public List<GameObject> enemiesAlive;
    private GameObject[] players;
    private GameObject[] enemies;

    private GameObject enemyLifes;
    private GameObject playerAvatarLifes;
    public GameObject life;
    public GameObject playerImage;
    public GameObject enemyImage;
    private GameObject instaLife;
    private Vector3 positionPlayerLife;
    private Vector3 positionEnemyLife;
    private float offsetLife;

    public int selectedPlayerAvatar;
    public int selectedEnemyAvatar;
    public int selectedPlayerTarget;
    public int selectedEnemyTarget;
    public string teamTurn;

    public string playerTag = "Player";
	public string enemiesTag = "Enemy";


    private GameObject redSprite;
    private GameObject blueSprite;
    private GameObject victory;
    private GameObject[] particles;
    public float chrono = 4;
    private bool chronoActive = false;

    private powerBarScript powerBar;
    private float distance;

    void Start () {

        players = GameObject.FindGameObjectsWithTag(playerTag);
        enemies = GameObject.FindGameObjectsWithTag(enemiesTag);

        enemyLifes = GameObject.FindGameObjectWithTag("enemyLifes");
        playerAvatarLifes = GameObject.FindGameObjectWithTag("playerAvatarLifes");
        positionEnemyLife = enemyLifes.transform.GetChild(0).position;
        positionPlayerLife = playerAvatarLifes.transform.GetChild(0).position;
        offsetLife = life.transform.localScale.x * 4;

        foreach (GameObject player in players)
        {
            playerAvatarsAlive.Add(player);
            //image
            instaLife = Instantiate(playerImage, positionPlayerLife, new Quaternion()) as GameObject;
            instaLife.transform.SetParent(playerAvatarLifes.transform);
            //XMark
            instaLife = Instantiate(life, positionPlayerLife, new Quaternion()) as GameObject;
            instaLife.transform.SetParent(playerAvatarLifes.transform);
            positionPlayerLife += new Vector3(offsetLife, 0, 0);
        }
        playerAvatarLifes.GetComponent<playersLivesScript>().enabled = true;
        foreach (GameObject enemy in enemies)
        {
            enemiesAlive.Add(enemy);
            //image
            instaLife = Instantiate(enemyImage, positionEnemyLife, new Quaternion()) as GameObject;
            instaLife.transform.SetParent(enemyLifes.transform);
            //XMark
            instaLife = Instantiate(life, positionEnemyLife, new Quaternion()) as GameObject;
            instaLife.transform.SetParent(enemyLifes.transform);
            positionEnemyLife -= new Vector3(offsetLife, 0, 0);
        }
        enemyLifes.GetComponent<enemiesLivesScript>().enabled = true;

        powerBar = GameObject.Find("powerBar").GetComponent<powerBarScript>();
        redSprite = GameObject.Find("powerBarRed");
        blueSprite = GameObject.Find("powerBarBlue");
        victory = GameObject.Find("victoryText");


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
        if (teamTurn == enemiesTag)
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
        else if (teamTurn == playerTag)
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
                StopAllCoroutines();
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
        if (chronoActive)
        {
            chrono -= Time.deltaTime;
        }
        if (chrono <= 0)
        {
            SceneManager.LoadScene("Menu");
        }
        if(teamTurn == playerTag)
        {
            distance = (playerAvatarsAlive[selectedPlayerAvatar].transform.position - enemiesAlive[selectedPlayerTarget].transform.position).magnitude;
            powerBar.ChangeSpeed(distance);
        }
        else if(teamTurn == enemiesTag)
        {
            distance = (enemiesAlive[selectedEnemyAvatar].transform.position - playerAvatarsAlive[selectedEnemyTarget].transform.position).magnitude;
            powerBar.ChangeSpeed(distance);
        }
	}

    public void changeSelectCharacter(string team)
    {
        if (team == playerTag)
        {
            if (teamTurn == playerTag)
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
            if (teamTurn == enemiesTag)
            {
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

    public void changeSelectTarget(string team, GameObject tartget = null)
    {
        if (team == playerTag)
        {
            if (tartget == null)
            {
                selectedPlayerTarget = (selectedPlayerTarget + 1) % enemiesAlive.Count;
            }
            else
            {
                selectedPlayerTarget = enemiesAlive.FindIndex(x => x.gameObject == tartget);
            }
        }
        if (team == enemiesTag)
        {
            if (tartget == null)
            {
                selectedEnemyTarget = (selectedEnemyTarget + 1) % playerAvatarsAlive.Count;
            }
            else
            {
                selectedEnemyTarget = playerAvatarsAlive.FindIndex(x => x.gameObject == tartget);
            }
        }
    }

    public GameObject GetSelectedCharacter(string team)
    {
        if (team == playerTag)
        {
            return playerAvatarsAlive[selectedPlayerAvatar];
        }
        else
        {
            return enemiesAlive[selectedEnemyAvatar];
        }
    }

    public GameObject GetTargetedCharacter(string team)
    {
        if (team == playerTag)
        {
            return enemiesAlive[selectedPlayerTarget];
        }
        else
        {
            return playerAvatarsAlive[selectedEnemyTarget];
        }
    }
}

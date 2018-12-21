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
    private GameObject victory2;
    private GameObject[] particles;
    public float chrono = 4;
    private bool gameEnded = false;

    private powerBarScript powerBar;
    private float distance;

    private AudioSource[] audioSources;
    private GameObject mainCamera;
    private AudioClip winClip;
    private AudioClip whipSound;
    private AudioClip clapSound;
    private AudioClip finalClip;
    private AudioClip deathPlayerSound;
    public bool playWinMusic = false;
    public bool finalKill = false;
    private IEnumerator playAudio = null;

    void Start()
    {

        players = GameObject.FindGameObjectsWithTag(playerTag);
        enemies = GameObject.FindGameObjectsWithTag(enemiesTag);
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        audioSources = mainCamera.GetComponents<AudioSource>();
        winClip = (AudioClip)Resources.Load("Audio/win");
        finalClip = (AudioClip)Resources.Load("Audio/final");
        whipSound = (AudioClip)Resources.Load("Audio/whip");
        clapSound = (AudioClip)Resources.Load("Audio/clap");
        deathPlayerSound = (AudioClip)Resources.Load("Audio/Shotgun_Blast-Jim_Rogers-1914772763");

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
        victory2 = GameObject.Find("victoryText2");


        //commencer la partie
        redSprite.SetActive(false);
        blueSprite.SetActive(true);
        victory.SetActive(false);
        victory2.SetActive(false);
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
                victory2.SetActive(true);
                if (!playWinMusic)
                {
                    playWinMusic = true;
                    if (playAudio != null)
                    {
                        StopCoroutine(playAudio);
                    }
                    playAudio = PlayWinMusic();
                    StartCoroutine(playAudio);
                }
                particles = GameObject.FindGameObjectsWithTag("RedFountain");
                foreach (GameObject emitter in particles)
                {
                    emitter.GetComponent<ParticleSystem>().Play();
                }
                gameEnded = true;
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
                victory.SetActive(true);
                victory2.SetActive(true);
                if (!playWinMusic)
                {
                    playWinMusic = true;
                    if (playAudio != null)
                    {
                        StopCoroutine(playAudio);
                    }
                    playAudio = PlayWinMusic();
                    StartCoroutine(playAudio);
                }
                particles = GameObject.FindGameObjectsWithTag("BlueFountain");
                foreach (GameObject emitter in particles)
                {
                    emitter.GetComponent<ParticleSystem>().Play();
                }
                gameEnded = true;
            }
        }

        if (playerAvatarsAlive.Count == 1 && enemiesAlive.Count == 1 && !finalKill)
        {
            finalKill = true;
            if (playAudio != null)
            {
                StopCoroutine(playAudio);
            }
            playAudio = PlayDuelMusic();
            StartCoroutine(playAudio);
        }
    }


    void Update()
    {
        if (gameEnded)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                SceneManager.LoadScene("Menu");
            }
        }

        if (teamTurn == playerTag)
        {
            if (playerAvatarsAlive.Count != 0 && enemiesAlive.Count != 0)
            {
                distance = (playerAvatarsAlive[selectedPlayerAvatar].transform.position - enemiesAlive[selectedPlayerTarget].transform.position).magnitude;
                powerBar.ChangeSpeed(distance);
            }
        }
        else if (teamTurn == enemiesTag)
        {
            if (enemiesAlive.Count != 0 && playerAvatarsAlive.Count != 0)
            {
                distance = (enemiesAlive[selectedEnemyAvatar].transform.position - playerAvatarsAlive[selectedEnemyTarget].transform.position).magnitude;
                powerBar.ChangeSpeed(distance);
            }
        }
    }

    private IEnumerator PlayWinMusic()
    {
        if (mainCamera.GetComponent<backGroudMusicScript>().play != null)
            StopCoroutine(mainCamera.GetComponent<backGroudMusicScript>().play);
        mainCamera.GetComponent<backGroudMusicScript>().enabled = false;
        audioSources[0].Stop();
        audioSources[2].clip = clapSound;
        audioSources[2].Play();
        yield return new WaitForSeconds(audioSources[2].clip.length-1);
        mainCamera.GetComponent<crossFaderScript>().CrossFade(audioSources[0], audioSources[0], winClip);
    }

    private IEnumerator PlayDuelMusic()
    {
        audioSources[0].Stop();
        yield return new WaitForSeconds(deathPlayerSound.length-1);
        if (mainCamera.GetComponent<backGroudMusicScript>().play != null)
            StopCoroutine(mainCamera.GetComponent<backGroudMusicScript>().play);
        mainCamera.GetComponent<backGroudMusicScript>().enabled = false;
        
        audioSources[2].clip = whipSound;
        audioSources[2].Play();
        yield return new WaitForSeconds(audioSources[2].clip.length);
        mainCamera.GetComponent<crossFaderScript>().CrossFade(audioSources[0], audioSources[0], finalClip);
    }

    public void changeSelectCharacter(string team, GameObject character = null)
    {
        if (team == playerTag)
        {
            if (teamTurn == playerTag)
            {
                StopCoroutine(playerAvatarsAlive[selectedPlayerAvatar].GetComponent<Launcher>().launch());
            }
            playerAvatarsAlive[selectedPlayerAvatar].GetComponent<controlPlayer>().enabled = false;

            if (character == null)
            {
                selectedPlayerAvatar = (selectedPlayerAvatar + 1) % playerAvatarsAlive.Count;
            }
            else
            {
                selectedPlayerAvatar = playerAvatarsAlive.FindIndex(x => x.gameObject == character);
            }

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

            if (character == null)
            {
                selectedEnemyAvatar = (selectedEnemyAvatar + 1) % enemiesAlive.Count;
            }
            else
            {
                selectedEnemyAvatar = enemiesAlive.FindIndex(x => x.gameObject == character);
            }

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseScript : MonoBehaviour {

    public static bool isGamePause = false;

    public GameObject pauseMenu;
    public GameObject volumeMenu;
    public GameObject mainCamera;
    public GameObject gameManager;

    private AudioClip menuClip;
    private AudioClip finalClip;
    AudioSource backGroudSource;

    void Awake()
    {
        menuClip = (AudioClip)Resources.Load("Audio/menuBG");
        finalClip = (AudioClip)Resources.Load("Audio/final");

        backGroudSource = mainCamera.GetComponents<AudioSource>()[0];
    }

    void Update () {

        //pause ou résumer
        if (Input.GetKeyDown(KeyCode.Escape) && !gameManager.GetComponent<GameManager>().playWinMusic)
        {
            mainCamera.transform.GetChild(0).GetComponents<AudioSource>()[1].Play();
            if (isGamePause)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        volumeMenu.SetActive(false);
        Time.timeScale = 1f;
        isGamePause = false;
        Cursor.visible = false;
        
        if(!gameManager.GetComponent<GameManager>().finalKill)
        {
            mainCamera.GetComponent<backGroudMusicScript>().enabled = true;
            mainCamera.GetComponent<backGroudMusicScript>().playNextMusic = true;
        }
        else
        {
            mainCamera.GetComponent<crossFaderScript>().CrossFade(backGroudSource, backGroudSource, finalClip);
        }
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isGamePause = true;
        Cursor.visible = true;

        if(!gameManager.GetComponent<GameManager>().finalKill)
        {
            if (mainCamera.GetComponent<backGroudMusicScript>().play != null)
                StopCoroutine(mainCamera.GetComponent<backGroudMusicScript>().play);
            mainCamera.GetComponent<backGroudMusicScript>().enabled = false;
        }
        mainCamera.GetComponent<crossFaderScript>().CrossFade(backGroudSource, backGroudSource, menuClip);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

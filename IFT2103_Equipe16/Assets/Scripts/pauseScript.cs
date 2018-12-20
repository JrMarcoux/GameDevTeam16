using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseScript : MonoBehaviour {

    public static bool isGamePause = false;

    public GameObject pauseMenu;
    public GameObject volumeMenu;
    public GameObject mainCamera;

    private AudioClip menuClip;
    AudioSource backGroudSource;

    void Awake()
    {
        menuClip = (AudioClip)Resources.Load("Audio/menuBG");
        backGroudSource = mainCamera.GetComponents<AudioSource>()[0];
    }

    void Update () {

        //pause ou résumer
        if (Input.GetKeyDown(KeyCode.Escape))
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

        backGroudSource.Stop();
        mainCamera.GetComponent<backGroudMusicScript>().enabled = true;
        mainCamera.GetComponent<backGroudMusicScript>().playNextMusic = true;
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isGamePause = true;

        if (mainCamera.GetComponent<backGroudMusicScript>().play != null)
            StopCoroutine(mainCamera.GetComponent<backGroudMusicScript>().play);
        mainCamera.GetComponent<backGroudMusicScript>().enabled = false;
        backGroudSource.Stop();
        backGroudSource.clip = menuClip;
        backGroudSource.Play();
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

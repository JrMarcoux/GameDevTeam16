using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class MainMenu : MonoBehaviour
{
    private int nbCharacters;
    private int nbEnemies;
    private int levelWidth;
    private int levelDepth;
    private int nbDecors;
    private bool difficultyHard;

    public TMPro.TextMeshProUGUI charTxt, enemiesTxt, widthTxt, depthTxt, decorsTxt;

    void Start()
    {
        nbCharacters = 3;
        nbEnemies = 3;
        levelWidth = 25;
        levelDepth = 25;
        nbDecors = 5;
        difficultyHard = false;

        if (charTxt != null)
        {
            charTxt.text = nbCharacters.ToString();
            enemiesTxt.text = nbEnemies.ToString();
            widthTxt.text = levelWidth.ToString();
            depthTxt.text = levelDepth.ToString();
            decorsTxt.text = nbDecors.ToString();
        }
    }

    public void PlayLocal()
    {
        PlayerPrefs.SetInt("nbCharacters", nbCharacters);
        PlayerPrefs.SetInt("nbEnemies", nbEnemies);
        PlayerPrefs.SetInt("levelWidth", levelWidth);
        PlayerPrefs.SetInt("levelDepth", levelDepth);
        PlayerPrefs.SetInt("nbDecors", nbDecors);
        PlayerPrefs.SetInt("difficulty", Convert.ToInt32(difficultyHard));
        PlayerPrefs.Save();
        ApplicationModel.sceneToLoad = "combatScene";
        SceneManager.LoadScene("loadingScene");
    }

    /* DEPRECATED multijoueur avec la classe Network
	public void PlayLanMultiplayer()
		{
            ApplicationModel.sceneToLoad = "multiplayerScene";
            SceneManager.LoadScene("loadingScene");
    }
  */


    //Multjoueur Photon
    public void PlayOnlineMultiplayer()
    {
        ApplicationModel.sceneToLoad = "PUNmultiplayerScene";
        SceneManager.LoadScene("loadingScene");
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void AdjustNbCharaters(float newInt)
    {
        nbCharacters = (int)newInt;
        charTxt.text = nbCharacters.ToString();
    }

    public void AdjustNbEnemies(float newInt)
    {
        nbEnemies = (int)newInt;
        enemiesTxt.text = nbEnemies.ToString();
    }

    public void AdjustLevelwidth(float newInt)
    {
        levelWidth = (int)newInt;
        widthTxt.text = levelWidth.ToString();
    }

    public void AdjustLevelDepth(float newInt)
    {
        levelDepth = (int)newInt;
        depthTxt.text = levelDepth.ToString();
    }

    public void AdjustNbDecors(float newInt)
    {
        nbDecors = (int)newInt;
        decorsTxt.text = nbDecors.ToString();
    }
    public void AdjustDifficulty(float newInt)
    {
        difficultyHard = Convert.ToBoolean(newInt);
    }

}

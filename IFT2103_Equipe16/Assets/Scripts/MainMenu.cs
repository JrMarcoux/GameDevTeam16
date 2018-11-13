using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour {
    private int nbCharacters;
    private int nbEnemies;
    private int levelWidth;
    private int levelDepth;
    private int nbDecors;

    public TMPro.TextMeshProUGUI charTxt, enemiesTxt, widthTxt, depthTxt, decorsTxt;

    void Start()
    {
        nbCharacters = 3;
        nbEnemies = 3;
        levelWidth = 25;
        levelDepth = 25;
        nbDecors = 5;

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
        PlayerPrefs.Save();
        SceneManager.LoadScene(1);
    }
	public void PlayMultiplayer()
		{
		    SceneManager.LoadScene(2);
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


}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemiesLivesScript : MonoBehaviour
{

    private List<Image> lives;
    private int index;
    private GameObject gameManager;
    private int nbrLives;

    // Use this for initialization
    void Start()
    {
        lives = new List<Image>();
        foreach (Transform child in transform)
        {
            if(child.GetComponent<Image>() != null)
            {
                lives.Add(child.GetComponent<Image>());
            }           
        }
        index = 0;
        gameManager = GameObject.FindGameObjectWithTag("Game manager");
        nbrLives = gameManager.GetComponent<GameManager>().enemiesAlive.Count;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.GetComponent<GameManager>().enemiesAlive.Count < nbrLives)
        {
            lives[index++].enabled = true;
            nbrLives--;
        }
    }
}

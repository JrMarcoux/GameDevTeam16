﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectedEnemyArrowScript : MonoBehaviour {

    private GameObject gameManager;
    public Vector3 focusPosition;

    void Start () {
        gameManager = GameObject.FindGameObjectWithTag("Game manager");
    }
	
	void Update () {
        focusPosition = gameManager.GetComponent<GameManager>().enemiesAlive[gameManager.GetComponent<GameManager>().selectedEnemyAvatar].transform.position;
        transform.position = new Vector3(0, 1, 0) + focusPosition;

    }
}

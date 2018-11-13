using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetEnemyArrowScript : MonoBehaviour {

    private GameObject gameManager;
    public Vector3 focusPosition;

    void Start () {
        gameManager = GameObject.FindGameObjectWithTag("Game manager");
    }
	
	void Update () {
        focusPosition = gameManager.GetComponent<GameManager>().playerAvatarsAlive[gameManager.GetComponent<GameManager>().selectedEnemyTarget].transform.position;
        transform.position = new Vector3(0, 2, 0) + focusPosition;

    }
}

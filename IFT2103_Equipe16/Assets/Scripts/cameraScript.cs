using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour {

    private GameObject gameManager;
    public Vector3 focusPosition;

    void Start () {
        gameManager = GameObject.FindGameObjectWithTag("Game manager");
    }
	
	void Update () {        
        if (gameManager.GetComponent<GameManager>().teamTurn == gameManager.GetComponent<GameManager>().playerTag)
        {
            if (gameManager.GetComponent<GameManager>().playerAvatarsAlive[gameManager.GetComponent<GameManager>().selectedPlayerAvatar].GetComponent<Launcher>().isProj)
            {
                focusPosition = gameManager.GetComponent<GameManager>().playerAvatarsAlive[gameManager.GetComponent<GameManager>().selectedPlayerAvatar].GetComponent<Launcher>().currentProj.transform.position;
            }
            else
            {
                focusPosition = gameManager.GetComponent<GameManager>().playerAvatarsAlive[gameManager.GetComponent<GameManager>().selectedPlayerAvatar].transform.position;
            }           
        }
        else if (gameManager.GetComponent<GameManager>().teamTurn == gameManager.GetComponent<GameManager>().enemiesTag)
        {
            if (gameManager.GetComponent<GameManager>().enemiesAlive[gameManager.GetComponent<GameManager>().selectedEnemyAvatar].GetComponent<Launcher>().isProj)
            {
                focusPosition = gameManager.GetComponent<GameManager>().enemiesAlive[gameManager.GetComponent<GameManager>().selectedEnemyAvatar].GetComponent<Launcher>().currentProj.transform.position;
            }
            else
            {
                focusPosition = gameManager.GetComponent<GameManager>().enemiesAlive[gameManager.GetComponent<GameManager>().selectedEnemyAvatar].transform.position;
            }           
        }
        transform.position = new Vector3(0, 1, -3) + focusPosition;

    }
}

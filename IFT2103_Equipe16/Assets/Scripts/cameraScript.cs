using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour {

    private GameObject gameManager;
    private Vector3 focusPosition;
    private int zoomPosition;
    private int heightPosition;

    void Start () {
        gameManager = GameObject.FindGameObjectWithTag("Game manager");
        zoomPosition = 10;
        heightPosition = 3;
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
        transform.position = new Vector3(0, heightPosition, -zoomPosition) + focusPosition;

    }

    public void Zoom()
    {
        if(zoomPosition > 2)
        {
            zoomPosition -= 1;
        }
        AdjustHeight();
    }

    public void Unzoom()
    {
        if (zoomPosition < 100)
        {
            zoomPosition += 1;
        }
        AdjustHeight();
    }

    void AdjustHeight()
    {
        if (zoomPosition <= 3)
        {
            heightPosition = 1;
        }
        else if (3 < zoomPosition && zoomPosition <= 6)
        {
            heightPosition = 2;
        }
        else if ( zoomPosition > 6)
        {
            heightPosition = 3;
        }
    }
}

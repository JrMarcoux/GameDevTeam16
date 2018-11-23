using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectedPlayerArrowScript : MonoBehaviour {

    private GameObject gameManager;
    public Vector3 focusPosition;

    void Start () {
        gameManager = GameObject.FindGameObjectWithTag("Game manager");
    }
	
	void Update () {
        if (gameManager.GetComponent<GameManager>().playerAvatarsAlive.Count != 0)
        {
            focusPosition = gameManager.GetComponent<GameManager>().playerAvatarsAlive[gameManager.GetComponent<GameManager>().selectedPlayerAvatar].transform.position;
            transform.position = new Vector3(0, 1, 0) + focusPosition;
        }
    }
}

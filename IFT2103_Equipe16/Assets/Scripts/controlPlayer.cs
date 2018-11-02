using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlPlayer : MonoBehaviour
{
    private GameObject gameManager;

    public float speed = 1f;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Game manager");
    }

    void Update()
    {
        if (Input.GetKey(GetKeyPrefs("Down")))
        {
            transform.Translate(0f, 0f, -speed * Time.deltaTime);
        }
        if (Input.GetKey(GetKeyPrefs("Up")))
        {
            transform.Translate(0f, 0f, speed * Time.deltaTime);
        }
        if (Input.GetKey(GetKeyPrefs("Right")))
        {
            transform.Translate(speed * Time.deltaTime, 0f, 0f);
        }
        if (Input.GetKey(GetKeyPrefs("Left")))
        {
            transform.Translate(-speed * Time.deltaTime, 0f, 0f);
        }
        if (Input.GetKeyDown(GetKeyPrefs("Fire")) && gameManager.GetComponent<GameManager>().teamTurn == gameManager.GetComponent<GameManager>().playerTag)
        {
            GetComponent<Launcher>().isShooting = true;
        }
        if (Input.GetKeyDown(GetKeyPrefs("ChgCharacter")))
        {
            gameManager.GetComponent<GameManager>().changeSelectCharacter(gameManager.GetComponent<GameManager>().playerTag);
        }
        if (Input.GetKeyDown(GetKeyPrefs("ChgTarget")))
        {
            gameManager.GetComponent<GameManager>().changeSelectTarget(gameManager.GetComponent<GameManager>().playerTag);
        }
    }

    public KeyCode GetKeyPrefs(string keyName)
    {
        return (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(keyName));
    }
}

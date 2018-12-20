using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlPlayer : MonoBehaviour
{
    private GameObject gameManager;
    private cameraScript cameraScript;

    public float speed = 1f;
    public float jumpSpeed = 3f;
    private float delayJump = 0;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Game manager");
        cameraScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<cameraScript>();
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
            this.GetComponents<AudioSource>()[0].Play();
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
        if (Input.GetKeyDown(GetKeyPrefs("Jump")) && Time.time > delayJump)
        {
            this.GetComponents<AudioSource>()[2].Play();
            GetComponent<Rigidbody>().velocity += jumpSpeed * Vector3.up;
            delayJump = Time.time + 0.8f;
        }
        if (Input.GetKey(GetKeyPrefs("Zoom")))
        {
            cameraScript.Zoom();
        }
        if (Input.GetKey(GetKeyPrefs("Unzoom")))
        {
            cameraScript.Unzoom();
        }
        transform.Translate(Input.GetAxis("Horizontal")*speed * Time.deltaTime, 0f, 0f);
        transform.Translate(0f, 0f, Input.GetAxis("Vertical")*speed * Time.deltaTime);
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            cameraScript.Zoom();
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            cameraScript.Unzoom();
        }
    }

    public KeyCode GetKeyPrefs(string keyName)
    {
        return (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(keyName));
    }
}

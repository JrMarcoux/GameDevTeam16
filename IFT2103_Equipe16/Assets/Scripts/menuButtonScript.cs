using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuButtonScript : MonoBehaviour
{

    public GameObject pointer;
    private GameObject mainCamera;

    void Awake()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    public void changePointerPosition()
    {
        pointer.transform.position = new Vector3(pointer.transform.position.x, transform.position.y, pointer.transform.position.z);
    }

    public void playOnEnterSound()
    {
        mainCamera.transform.GetChild(0).GetComponents<AudioSource>()[0].Play();
    }

    public void playOnClickSound()
    {
        mainCamera.transform.GetChild(0).GetComponents<AudioSource>()[1].Play();
    }
}

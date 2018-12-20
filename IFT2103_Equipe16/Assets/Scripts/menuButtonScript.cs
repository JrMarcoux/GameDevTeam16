using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuButtonScript : MonoBehaviour
{

    public GameObject pointer;
    private GameObject camera;

    void Awake()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    public void changePointerPosition()
    {
        pointer.transform.position = new Vector3(pointer.transform.position.x, transform.position.y, pointer.transform.position.z);
    }

    public void playOnEnterSound()
    {
        camera.transform.GetChild(0).GetComponents<AudioSource>()[0].Play();
    }

    public void playOnClickSound()
    {
        camera.transform.GetChild(0).GetComponents<AudioSource>()[1].Play();
    }
}

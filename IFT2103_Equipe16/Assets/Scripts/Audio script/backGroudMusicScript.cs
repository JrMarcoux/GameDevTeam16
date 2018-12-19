using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backGroudMusicScript : MonoBehaviour
{
    private AudioClip[] soundClips;
    private bool playNextMusic;
    private int musicNumber;
    private AudioSource backGroundMusicSource;
    public Coroutine play = null;

    // Use this for initialization
    void Awake()
    {
        soundClips = new AudioClip[3];
        soundClips[0] = (AudioClip)Resources.Load("Audio/BG1");
        soundClips[1] = (AudioClip)Resources.Load("Audio/BG2");
        soundClips[2] = (AudioClip)Resources.Load("Audio/BG3");
        backGroundMusicSource = GetComponents<AudioSource>()[0];
        playNextMusic = true;
        musicNumber = -1;
    }

    // Update is called once per frame
    void Update()
    {

        if (playNextMusic)
        {
            if (play != null)
            {
                StopCoroutine(play);
            }
            play = StartCoroutine(PlayTheNextMusic());
        }
    }

    private IEnumerator PlayTheNextMusic()
    {
        playNextMusic = false;
        int randomNbr = Random.Range(0, 2);
        if (musicNumber == randomNbr) yield return null;
        else
        {
            musicNumber = randomNbr;
            Debug.Log("found nbr");
            backGroundMusicSource.clip = soundClips[musicNumber];
            backGroundMusicSource.Play();
            yield return new WaitForSeconds(backGroundMusicSource.clip.length);
            playNextMusic = true;
        }
    }
}

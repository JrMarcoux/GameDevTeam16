using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crossFaderScript : MonoBehaviour
{
    public GameObject mainCamera;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator CrossFade(AudioSource from, AudioSource to, AudioClip clip = null)
    {
        IEnumerator crossFade = CFAction(from, to, clip);

        StartCoroutine(crossFade);

        return crossFade;
    }

    private IEnumerator CFAction(AudioSource from, AudioSource to, AudioClip clip = null)
    {
        float initialFromVolume = from.volume;
        float initialToVolume = to.volume;

        for (float i = initialFromVolume; i > 0; i = i - 0.1f)
        {
            from.volume = i;
            yield return null;
        }
        from.volume = 0;
        from.Stop();

        if (clip != null)
        {
            to.clip = clip;
        }

        to.volume = 0;
        to.Play();
        for (float i = 0; i <= initialToVolume; i = i + 0.1f)
        {
            to.volume = i;
            yield return null;
        }
        to.volume = initialToVolume;
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class modifyVolumeScript : MonoBehaviour
{
    public GameObject bgSlider, fSlider, sfxSlider;
    public TMPro.TextMeshProUGUI bgNumber, fNumber, sfxNumber;
    public GameObject mainCamera;
    public GameObject gameManager;

    // Use this for initialization
    void Start()
    {
        bgSlider.GetComponent<Slider>().value = mainCamera.GetComponents<AudioSource>()[0].volume * 100;
        fSlider.GetComponent<Slider>().value = mainCamera.GetComponents<AudioSource>()[1].volume * 100;
        sfxSlider.GetComponent<Slider>().value = mainCamera.GetComponents<AudioSource>()[2].volume * 100;

        bgNumber.text = bgSlider.GetComponent<Slider>().value.ToString();
        fNumber.text = fSlider.GetComponent<Slider>().value.ToString();
        sfxNumber.text = sfxSlider.GetComponent<Slider>().value.ToString();
    }

    public void AdjustBGMVolume(float value)
    {
        mainCamera.GetComponents<AudioSource>()[0].volume = value / 100;
        bgNumber.text = value.ToString();
    }

    public void AdjustFoleysVolume(float value)
    {
        mainCamera.GetComponents<AudioSource>()[1].volume = value / 100;
        fNumber.text = value.ToString();
    }

    public void AdjustSFXVolume(float value)
    {
        mainCamera.GetComponents<AudioSource>()[2].volume = value / 100;
        mainCamera.transform.GetChild(0).GetComponents<AudioSource>()[0].volume = value / 100;
        mainCamera.transform.GetChild(0).GetComponents<AudioSource>()[1].volume = value / 100;

        List<GameObject> objectsAlive = gameManager.GetComponent<GameManager>().enemiesAlive
            .Concat(gameManager.GetComponent<GameManager>().playerAvatarsAlive).ToList();

        foreach (var objectAlive in objectsAlive)
        {
            AudioSource[] sources = objectAlive.GetComponents<AudioSource>();
            foreach (var sound in sources)
            {
                sound.volume = value / 100;
            }
        }
        sfxNumber.text = value.ToString();
    }
}

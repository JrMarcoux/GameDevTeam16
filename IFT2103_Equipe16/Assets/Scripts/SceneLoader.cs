using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    
    private string sceneToLoad;
    private float t;
    private float minTime;
    private AsyncOperation async;
    private Image progressBar;


    // Use this for initialization
    void Start()
    {
        sceneToLoad = ApplicationModel.sceneToLoad;
        progressBar = this.GetComponent<Image>();
        StartCoroutine(LoadNewScene());
    }

    IEnumerator LoadNewScene()
    {
        // load la scène de façons asynchrone
        async = SceneManager.LoadSceneAsync(sceneToLoad);

        while (!async.isDone)
        {
            //fait progresser la barre de progression en fonction de la scène à loader
            progressBar.fillAmount = Mathf.Clamp01(async.progress / 0.9f);

            yield return null;
        }
        yield return null;
    }
}

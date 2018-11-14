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
        minTime = 2f;
        t = 0;
        progressBar = this.GetComponent<Image>();
        StartCoroutine(LoadNewScene());
    }

    IEnumerator LoadNewScene()
    {
        // load la scène de façons asynchrone
        async = SceneManager.LoadSceneAsync(sceneToLoad);

        //empêche la scène de démarrer avant le temps minimal d'attente
        async.allowSceneActivation = false;

        while (!async.isDone)
        {
            //fait progresser la barre de progression en fonction de la scène à loader
            progressBar.fillAmount = Mathf.Clamp01(async.progress / 0.9f);
            t += Time.deltaTime;

            //vérifie si le temps minimal d'attente est terminé
            if (t > minTime)
            {
                async.allowSceneActivation = true;
            }

            yield return null;
        }
        yield return null;
    }
}

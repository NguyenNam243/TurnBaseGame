using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneManager : MonoBehaviour
{
    public Slider loadingSlider = null;
    public TMP_Text loadingText = null;


    private void Start()
    {
        StartCoroutine(LoadSenceAsync());
    }

    public IEnumerator LoadSenceAsync()
    {
        AsyncOperation handle = SceneManager.LoadSceneAsync(LoadSceneExtension.sceneToLoad);
        while (!handle.isDone)
        {
            yield return new WaitForSeconds(1);
            loadingSlider.value = handle.progress;
            loadingText.text = "Loading... " + handle.progress + "%";
        }
    }
}

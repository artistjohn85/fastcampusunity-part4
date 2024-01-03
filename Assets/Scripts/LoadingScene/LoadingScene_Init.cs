using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene_Init : MonoBehaviour
{
    private LoadingScene_UI loadingScene_UI;
    private SceneLoadManager sceneLoadManager;

    private void Awake()
    {
        loadingScene_UI = FindAnyObjectByType<LoadingScene_UI>();
        sceneLoadManager = FindAnyObjectByType<SceneLoadManager>();
    }

    private void Start()
    {
        StartCoroutine(LoadSceneAsync(sceneLoadManager.AfterSceneType));   
    }

    private IEnumerator LoadSceneAsync(SCENE_TYPE sCENE_TYPE)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sCENE_TYPE.ToString());
        //asyncOperation.progress // 0~0.9

        while (!asyncOperation.isDone) 
        {
            // Debug.Log(asyncOperation.progress);
            loadingScene_UI.SetPercent(asyncOperation.progress / 0.9f);
            yield return null;
        }
    }
}

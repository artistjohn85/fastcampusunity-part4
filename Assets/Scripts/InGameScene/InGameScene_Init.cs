using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameScene_Init : MonoBehaviour
{
    private InGameScene_UI inGameScene_UI; // cache
    private LocalizationManager localizationManager; // cache

    private void Awake()
    {
        if (!SystemManager.Instance.IsInit)
        {
            SceneLoadManager.Instance.GoInitAndReturnScene(SCENE_TYPE.InGame);
            return;
        }

        inGameScene_UI = FindAnyObjectByType<InGameScene_UI>();
        localizationManager = FindAnyObjectByType<LocalizationManager>();

        Debug.Log(DataManager.Instance.UserController.UserConfigInfo.UserName);
    }

    private void Start()
    {
        string value = localizationManager.GetString(1);
        inGameScene_UI.SetTitle(value);
    }
}

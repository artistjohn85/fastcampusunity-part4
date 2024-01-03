using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitScene_Init : MonoBehaviour
{
    private const int PROGRESS_VALUE = 5;
    private int progressAddValue = 0;

    private SystemManager systemManager; // cache
    private InitScene_UI InitScene_UI; // cache
    private ObjectPoolManager objectPoolManager; // cache
    private EffectManager effectManager; // cache
    private SoundManager soundManager; // cache
    private WindowManager windowManager; // cahe

    private void Awake()
    {
        //SystemManager[] effectManagers = FindObjectsByType<SystemManager>(FindObjectsSortMode.None);
        //Debug.Log("InitScene_Init Length:" + effectManagers.Length);
    }

    private IEnumerator Start()
    {
        yield return null;
        systemManager = FindAnyObjectByType<SystemManager>();
        Debug.Log("InitScene_Init IsInit: " + systemManager.IsInit);
        InitScene_UI = FindAnyObjectByType<InitScene_UI>();
        objectPoolManager = FindAnyObjectByType<ObjectPoolManager>();
        effectManager = FindAnyObjectByType<EffectManager>();
        soundManager = FindAnyObjectByType<SoundManager>();
        windowManager = FindAnyObjectByType<WindowManager>();

        StartCoroutine(C_Manager());
    }

    private IEnumerator C_Manager()
    {
        List<Action> actions = new List<Action>
        {
            SystemManagerInit,
            ObjectPoolManagerInit,
            EffectManagerInit,
            SoundManager,
            WindowManagerInit,
            LoadScene,
        };

        foreach (Action action in actions)
        {
            yield return new WaitForSeconds(0.1f);
            action?.Invoke();
            SetProgress();
        }
    }

    private void SetProgress()
    {
        InitScene_UI.SetPercent((float)++progressAddValue / (float)PROGRESS_VALUE);
    }

    private void SystemManagerInit()
    {
        systemManager.SetInit();
    }

    private void ObjectPoolManagerInit()
    {
        objectPoolManager.SetInit();
    }

    private void EffectManagerInit()
    {
        effectManager.SetInit();
    }

    private void SoundManager()
    {
        soundManager.SetInit();
    }

    private void WindowManagerInit()
    {
        windowManager.SetInit();
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(SCENE_TYPE.Lobby.ToString());
    }
}

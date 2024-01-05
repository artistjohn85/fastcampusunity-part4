using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitScene_Init : MonoBehaviour
{
    private static bool isInit = false;
    
    private const int PROGRESS_VALUE = 5;
    private int progressAddValue = 0;

    private InitScene_UI InitScene_UI; // cache
    private ObjectPoolManager objectPoolManager; // cache
    private EffectManager effectManager; // cache
    private SoundManager soundManager; // cache
    private WindowManager windowManager; // cache
    private NetworkManager networkManager; // cache

    private void Awake()
    {
        InitScene_UI = FindAnyObjectByType<InitScene_UI>();

        if (!isInit)
        {
            isInit = true;
            objectPoolManager = new GameObject("ObjectPoolManager").AddComponent<ObjectPoolManager>();
            effectManager = new GameObject("EffectManager").AddComponent<EffectManager>();
            soundManager = new GameObject("SoundManager").AddComponent<SoundManager>();
            windowManager = new GameObject("WindowManager").AddComponent<WindowManager>();
            networkManager = new GameObject("NetworkManager").AddComponent<NetworkManager>();
        }
        else
        {
            objectPoolManager = FindAnyObjectByType<ObjectPoolManager>();
            effectManager = FindAnyObjectByType<EffectManager>();
            soundManager = FindAnyObjectByType<SoundManager>();
            windowManager = FindAnyObjectByType<WindowManager>();
            networkManager = FindAnyObjectByType<NetworkManager>();
        }
    }

    private IEnumerator Start()
    {
        yield return null;
        

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
            SceneLoadManagerInit,
            NetworkManagerInit,
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
        SystemManager.Instance.SetInit();
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

    private void SceneLoadManagerInit()
    {
        SceneLoadManager.Instance.SetInit();
    }

    private void NetworkManagerInit()
    {
        networkManager.SetInit(apiUrl: Config.SERVER_API_URL);

        ApplicationConfigSendPacket applicationConfigSendPacket
            = new ApplicationConfigSendPacket(PACKET_NAME_TYPE.ApplicationConfig,
            Config.E_ENVIRONMENT_TYPE,
            Config.E_OS_TYPE,
            Config.APP_VERSION);

        networkManager.SendPacket(applicationConfigSendPacket);
    }

    private void LoadScene()
    {
        SceneLoadManager.Instance.SceneLoad(SceneLoadManager.Instance.InitSceneType);
    }
}

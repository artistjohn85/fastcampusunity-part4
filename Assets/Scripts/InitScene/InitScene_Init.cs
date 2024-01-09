using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitScene_Init : MonoBehaviour
{
    [SerializeField] private GameObject prefabPopupMessage;
    [SerializeField] private Transform parentPopupMessage;

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
        IEnumerator enumerator = NetworkManagerInit();
        yield return StartCoroutine(enumerator);
        bool isNetworkManagerSuccess = (bool)enumerator.Current;
        if (!isNetworkManagerSuccess)
        {
            Debug.Log("��������, �ȳ�â ����ֱ�");
            GameObject objPopupMessage = Instantiate(prefabPopupMessage, parentPopupMessage);

            PopupMessageInfo popupMessageInfo = new PopupMessageInfo(POPUP_MESSAGE_TYPE.ONE_BUTTON, "��������", "���������� �߻��Ͽ����ϴ�. �ٽ� �����Ͽ��ּ���.");
            PopupMessage popupMessage = objPopupMessage.GetComponent<PopupMessage>();
            popupMessage.OpenMessage(popupMessageInfo, null, () =>
            {
                // �� ����
                Application.Quit();
            });
            yield break;
        }

        yield return new WaitForSeconds(0.1f);
        SetProgress();

        yield return StartCoroutine(EtcManager());

    }

    private IEnumerator EtcManager()
    {

        List<Action> actions = new List<Action>
        {
            SystemManagerInit,
            ObjectPoolManagerInit,
            EffectManagerInit,
            SoundManager,
            WindowManagerInit,
            SceneLoadManagerInit,
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

    private IEnumerator NetworkManagerInit()
    {
        networkManager.SetInit();

        ApplicationConfigSendPacket applicationConfigSendPacket
            = new ApplicationConfigSendPacket(
                Config.SERVER_APP_CONFIG_URL,
                PACKET_NAME_TYPE.ApplicationConfig,
                Config.E_ENVIRONMENT_TYPE,
                Config.E_OS_TYPE,
                Config.APP_VERSION);

        // Github�� ���ε�, �ݹ� ��� ���
        //networkManager.C_SendPacket<ApplicationConfigReceivePacket>(applicationConfigSendPacket, AppConfig);

        IEnumerator enumerator = networkManager.C_SendPacket<ApplicationConfigReceivePacket>(applicationConfigSendPacket);
        yield return StartCoroutine(enumerator);
        ApplicationConfigReceivePacket receivePacket = enumerator.Current as ApplicationConfigReceivePacket;
        if (receivePacket != null && receivePacket.ReturnCode == (int)RETURN_CODE.Success)
        {
            SystemManager.Instance.ApiUrl = receivePacket.ApiUrl;
            yield return true;
        }
        else
        {
            yield return false;
        }
    }

    // Github�� ���ε�, �ݹ� ��� ���
    //private void AppConfig(ReceivePacketBase receivePacketBase)
    //{
    //    ApplicationConfigReceivePacket receivePacket = receivePacketBase as ApplicationConfigReceivePacket;
    //    if (receivePacket != null && receivePacket.ReturnCode == (int)RETURN_CODE.Success)
    //    {
    //        SystemManager.Instance.ApiUrl = receivePacket.ApiUrl;
    //        Debug.Log("����"); // �״��� ������ ���⼭ ����
    //        StartCoroutine(EtcManager());
    //    }
    //    else
    //    {
    //        Debug.Log("���� �߻�"); // ���� �˾��� ���� ����
    //    }
    //}

    private void LoadScene()
    {
        SceneLoadManager.Instance.SceneLoad(SceneLoadManager.Instance.InitSceneType);
    }
}

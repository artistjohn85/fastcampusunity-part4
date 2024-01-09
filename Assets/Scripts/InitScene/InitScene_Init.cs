using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitScene_Init : MonoBehaviour
{
    [SerializeField] private GameObject prefabPopupMessage;
    [SerializeField] private GameObject prefabPopupInputMessage;
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
        DevelopmentIdPopup();
    }

    private void DevelopmentIdPopup()
    {
        GameObject objPopupInputMessage = Instantiate(prefabPopupInputMessage, parentPopupMessage);

        PopupMessageInfo popupMessageInfo = new PopupMessageInfo(POPUP_MESSAGE_TYPE.TWO_BUTTON, "DEVELOPMENT ID", "테스를 위한 ID를 입력해주세요.");
        PopupInputMessage popupInputMessage = objPopupInputMessage.GetComponent<PopupInputMessage>();
        popupInputMessage.OpenMessage(popupMessageInfo, SystemManager.Instance.DevelopmentId,
        () =>
        {
            StartCoroutine(C_Manager());
        },
        (inputFieldValue) =>
        {
            SystemManager.Instance.DevelopmentId = inputFieldValue;
            StartCoroutine(C_Manager());
        });
    }

    private IEnumerator C_Manager()
    {
        IEnumerator enumerator = NetworkManagerInit();
        yield return StartCoroutine(enumerator);
        bool isNetworkManagerSuccess = (bool)enumerator.Current;
        if (!isNetworkManagerSuccess)
        {
            Debug.Log("서버오류, 안내창 띄어주기");
            GameObject objPopupMessage = Instantiate(prefabPopupMessage, parentPopupMessage);

            PopupMessageInfo popupMessageInfo = new PopupMessageInfo(POPUP_MESSAGE_TYPE.ONE_BUTTON, "서버오류", "서버오류가 발생하였습니다. 다시 접속하여주세요.");
            PopupMessage popupMessage = objPopupMessage.GetComponent<PopupMessage>();
            popupMessage.OpenMessage(popupMessageInfo, null, () =>
            {
                // 앱 종료
                Application.Quit();
            });
            yield break;
        }

        yield return new WaitForSeconds(0.1f);
        SetProgress();

        if (SystemManager.Instance.dEVELOPMENT_ID_AUTHORITY == DEVELOPMENT_ID_AUTHORITY.None)
        {
            // 점검
            // 해당 패킷에서 오류가 발생하더라도, 계속 진행하도록 처리하도록 한다.
            IEnumerator eMaintenance = MaintenancePacket();
            yield return StartCoroutine(eMaintenance);
            MaintenanceReceivePacket maintenanceReceivePacket = eMaintenance.Current as MaintenanceReceivePacket;
            if (maintenanceReceivePacket != null && maintenanceReceivePacket.IsMaintenance) 
            {
                GameObject objPopupMessage = Instantiate(prefabPopupMessage, parentPopupMessage);

                PopupMessageInfo popupMessageInfo = new PopupMessageInfo(POPUP_MESSAGE_TYPE.ONE_BUTTON,
                    maintenanceReceivePacket.Title, maintenanceReceivePacket.Contents);
                PopupMessage popupMessage = objPopupMessage.GetComponent<PopupMessage>();
                popupMessage.OpenMessage(popupMessageInfo, null, () =>
                {
                    // 앱 종료
                    Debug.Log("점검으로 인한 OK버튼, 앱 종료");
                    Application.Quit();
                });
                yield break;
            }
        }

        yield return StartCoroutine(EtcManager());

    }

    private IEnumerator MaintenancePacket()
    {
        MaintenanceSendPacket sendPacket = new MaintenanceSendPacket(
               SystemManager.Instance.ApiUrl,
               PACKET_NAME_TYPE.Maintenance,
               Config.E_ENVIRONMENT_TYPE,
               Config.E_OS_TYPE,
               Config.APP_VERSION,
               Application.systemLanguage);

        IEnumerator enumerator = networkManager.C_SendPacket<MaintenanceReceivePacket>(sendPacket);
        yield return StartCoroutine(enumerator);
        yield return enumerator.Current;
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
                Config.APP_VERSION,
                SystemManager.Instance.DevelopmentId);

        // Github에 업로드, 콜백 방식 사용
        //networkManager.C_SendPacket<ApplicationConfigReceivePacket>(applicationConfigSendPacket, AppConfig);

        IEnumerator enumerator = networkManager.C_SendPacket<ApplicationConfigReceivePacket>(applicationConfigSendPacket);
        yield return StartCoroutine(enumerator);
        ApplicationConfigReceivePacket receivePacket = enumerator.Current as ApplicationConfigReceivePacket;
        if (receivePacket != null && receivePacket.ReturnCode == (int)RETURN_CODE.Success)
        {
            SystemManager.Instance.ApiUrl = receivePacket.ApiUrl;
            SystemManager.Instance.dEVELOPMENT_ID_AUTHORITY = (DEVELOPMENT_ID_AUTHORITY)receivePacket.DevelopmentIdAuthority;
            yield return true;
        }
        else
        {
            yield return false;
        }
    }

    // Github에 업로드, 콜백 방식 사용
    //private void AppConfig(ReceivePacketBase receivePacketBase)
    //{
    //    ApplicationConfigReceivePacket receivePacket = receivePacketBase as ApplicationConfigReceivePacket;
    //    if (receivePacket != null && receivePacket.ReturnCode == (int)RETURN_CODE.Success)
    //    {
    //        SystemManager.Instance.ApiUrl = receivePacket.ApiUrl;
    //        Debug.Log("성공"); // 그다음 순서를 여기서 실행
    //        StartCoroutine(EtcManager());
    //    }
    //    else
    //    {
    //        Debug.Log("에러 발생"); // 에러 팝업을 띄우고 종료
    //    }
    //}

    private void LoadScene()
    {
        SceneLoadManager.Instance.SceneLoad(SceneLoadManager.Instance.InitSceneType);
    }
}

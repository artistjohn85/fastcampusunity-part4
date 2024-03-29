using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemManager : ManagerBase
{
    private static SystemManager instance;
    public static SystemManager Instance
    {
        get
        {
            if (instance == null)
                instance = new GameObject("SystemManager").AddComponent<SystemManager>();

            return instance;
        }
    }

    public bool IsInit { get; set; } = false;
    public string ApiUrl { get; set; } = string.Empty;

    public string DevelopmentId 
    { 
        get
        {
            return PlayerPrefs.GetString("DevelopmentId");
        } 
        set
        {
            PlayerPrefs.SetString("DevelopmentId", value);
        }
    }

    public string CustomLanauge
    {
        get
        {
            return PlayerPrefs.GetString("CustomLanauge");
        }
        set
        {
            PlayerPrefs.SetString("CustomLanauge", value);
        }
    }

    public DEVELOPMENT_ID_AUTHORITY dEVELOPMENT_ID_AUTHORITY { get; set; } = DEVELOPMENT_ID_AUTHORITY.None;

    private void Awake()
    {
        Dontdestory<SystemManager>();
    }

    public void SetInit()
    {
        IsInit = true;
    }

}

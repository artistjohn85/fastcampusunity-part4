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

    private void Awake()
    {
        Dontdestory<SystemManager>();
    }

    public void SetInit()
    {
        IsInit = true;
    }

}

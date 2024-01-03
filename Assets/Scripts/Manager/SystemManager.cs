using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemManager : ManagerBase
{
    public bool IsInit { get; set; } = false;

    private void Awake()
    {
        Debug.Log("call SystemManager");

        Dontdestory<SystemManager>();
    }

    public void SetInit()
    {
        IsInit = true;
    }

}

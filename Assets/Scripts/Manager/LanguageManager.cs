using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageManager : ManagerBase
{
    private void Awake()
    {
        Dontdestory<LanguageManager>();
    }

    public void SetInit()
    {
    }

    public SystemLanguage Language
    {
        get
        {
            if (!string.IsNullOrEmpty(SystemManager.Instance.CustomLanauge))
            {
                string customLanauge = SystemManager.Instance.CustomLanauge;
                if (Enum.TryParse(customLanauge, out SystemLanguage language))
                    return language;
            }

            return Application.systemLanguage;
        }

        set
        {
            SystemManager.Instance.CustomLanauge = value.ToString();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LocalizationManager : ManagerBase
{
    private List<TableLocalization> localizations;

    private void Awake()
    {
        Dontdestory<LocalizationManager>();
    }

    public void SetInit(List<TableLocalization> localizations)
    {
        this.localizations = localizations;
    }

    public string GetString(int key)
    {
        TableLocalization tableLocalization = localizations.Where(v => v.key == key).FirstOrDefault();
        if (tableLocalization == null)
            return string.Empty;

        switch (Application.systemLanguage) 
        {
            case SystemLanguage.English:
                return tableLocalization.en;
            case SystemLanguage.Korean:
                return tableLocalization.kr;
            default:
                return tableLocalization.en;
        }
    }
}

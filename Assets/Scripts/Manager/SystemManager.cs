using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemManager : MonoBehaviour
{
    public bool IsInit { get; set; } = false;

    public void SetInit()
    {
        IsInit = true;
    }

}

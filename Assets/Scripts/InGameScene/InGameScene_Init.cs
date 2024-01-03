using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameScene_Init : MonoBehaviour
{
    private void Awake()
    {
        if (!SystemManager.Instance.IsInit)
            SceneLoadManager.Instance.GoInitAndReturnScene(SCENE_TYPE.InGame);
    }
}

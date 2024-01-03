using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyScene_Init : MonoBehaviour
{
    private void Awake()
    {
        if (!SystemManager.Instance.IsInit)
            SceneLoadManager.Instance.GoInitAndReturnScene(SCENE_TYPE.Lobby);

        //yield return new WaitForSeconds(1f);
        //SceneManager.LoadScene(SCENE_TYPE.Init.ToString());
    }
}

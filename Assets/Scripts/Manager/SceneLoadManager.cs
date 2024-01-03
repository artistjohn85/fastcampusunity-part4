using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : ManagerBase
{
    private static SceneLoadManager instance;
    public static SceneLoadManager Instance
    {
        get
        {
            if (instance == null)
                instance = new GameObject("SceneLoadManager").AddComponent<SceneLoadManager>();

            return instance;
        }
    }

    public SCENE_TYPE InitSceneType { get; set; } = SCENE_TYPE.Lobby;
    public SCENE_TYPE AfterSceneType { get; set; }

    private void Awake()
    {
        Dontdestory<SceneLoadManager>();
    }

    public void SetInit()
    {

    }

    public void GoInitAndReturnScene(SCENE_TYPE sCENE_TYPE)
    {
        this.InitSceneType = sCENE_TYPE;
        SceneManager.LoadScene(SCENE_TYPE.Init.ToString());
    }

    public void SceneLoad(SCENE_TYPE sCENE_TYPE)
    {
        this.AfterSceneType = sCENE_TYPE;
        SceneManager.LoadScene(SCENE_TYPE.Loading.ToString());
    }
}

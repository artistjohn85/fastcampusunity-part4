using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : ManagerBase
{
    public SCENE_TYPE AfterSceneType { get; set; }

    private void Awake()
    {
        Dontdestory<SceneLoadManager>();
    }

    public void SetInit()
    {

    }

    public void SceneLoad(SCENE_TYPE sCENE_TYPE)
    {
        this.AfterSceneType = sCENE_TYPE;
        SceneManager.LoadScene(SCENE_TYPE.Loading.ToString());
    }
}

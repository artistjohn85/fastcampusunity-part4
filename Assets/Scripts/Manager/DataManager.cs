using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DataManager : ManagerBase
{
    private static DataManager instance;
    public static DataManager Instance
    {
        get
        {
            if (instance == null)
                instance = new GameObject("DataManager").AddComponent<DataManager>();

            return instance;
        }
    }

    private UserController userController;

    public UserController UserController
    {   
        get { return userController; }
        set { userController = value; }
    }

    // serverdata
    // tabledata

    private void Awake()
    {
        Dontdestory<DataManager>();
    }

    public void SetInit(UserController userController)
    {
        this.userController = userController;
    }
}

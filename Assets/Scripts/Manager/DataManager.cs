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
    }

    // serverdata
    // tabledata

    private TableController tableController;
    public TableController TableController
    {
        get { return tableController; }
    }

    private void Awake()
    {
        Dontdestory<DataManager>();
    }

    public void SetInit(UserController userController, TableController tableController)
    {
        this.userController = userController;
        this.tableController = tableController;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class LocalStoreManager : ManagerBase
{
    private string dathPath;

    private void Awake()
    {
        Dontdestory<LocalStoreManager>();
    }

    public void SetInit(string dathPath)
    {
        this.dathPath = dathPath;
    }

    public void SaveData<T>(T data) where T : StoreBase
    {
        // JSON으로 저장하는 방식
        //string jsonStr = JsonUtility.ToJson(data);
        //string filePath = this.dathPath + $"/{typeof(T).Name}.json";
        //File.WriteAllText(filePath, jsonStr);

        //Debug.Log($"Save Completed : {filePath}");

        // 직렬화로 저장하는 방식
        string filePath = this.dathPath + $"/{typeof(T).Name}.dat";
        Debug.Log($"Save Completed : {filePath}");

        //FileStream file = File.Create(filePath);
        //BinaryFormatter bf = new BinaryFormatter();
        //bf.Serialize(file, data);
        //file.Close();

        // using문의 사용 & 에러 처리를 확실하게하자.
        try
        {
            using (FileStream file = File.Create(filePath))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(file, data);
            }
        }
        catch(Exception e)
        {
            Debug.LogError("SaveData: " + e.Message);
        }
    }

    public T LoadData<T>() where T : StoreBase
    {
        // JSON방식으로 로드
        //string filePath = this.dathPath + $"/{typeof(T).Name}.json";

        //if (!File.Exists(filePath))
        //    return null;

        //string fileStr = File.ReadAllText(filePath);
        //T data = JsonUtility.FromJson<T>(fileStr);
        //if (data != null)
        //    return data;

        //return null;

        // 직렬화 방식으로 로드
        string filePath = this.dathPath + $"/{typeof(T).Name}.dat";

        if (!File.Exists(filePath))
            return null;

        //FileStream file = File.Open(filePath, FileMode.Open);
        //BinaryFormatter bf = new BinaryFormatter();
        //T data = bf.Deserialize(file) as T;
        //file.Close();
        //return data;

        // using문의 사용 & 에러 처리를 확실하게하자.
        try
        {
            using (FileStream file = File.Open(filePath, FileMode.Open))
            {
                BinaryFormatter bf = new BinaryFormatter();
                T data = bf.Deserialize(file) as T;
                return data;
            }
        }
        catch (Exception e)
        {
            Debug.LogError("LoadData: " + e.Message);
            return null;
        }
    }

}

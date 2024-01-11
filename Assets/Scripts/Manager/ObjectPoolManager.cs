using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPoolManager : ManagerBase
{
    private Dictionary<int, List<GameObject>> dics = new Dictionary<int, List<GameObject>>();

    private void Awake()
    {
        Dontdestory<ObjectPoolManager>();
    }

    public void SetInit()
    {
    }

    public GameObject GetObjectByPrefab(GameObject prefab, Transform parent, Vector3 position, Quaternion rotation)
    {
        int hashCode = prefab.GetHashCode();
        Debug.Log(hashCode);

        if (!dics.ContainsKey(hashCode))
            dics.Add(hashCode, new List<GameObject>());

        List<GameObject> gameObjects = dics[hashCode];
        foreach (var item in gameObjects)
        {
            if (!item.gameObject.activeInHierarchy)
            {
                item.transform.localPosition = position;
                item.transform.localRotation = rotation;
                item.gameObject.SetActive(true); // active
                return item;
            }
        }

        GameObject obj = Instantiate(prefab, parent);
        obj.transform.localPosition = position;
        obj.transform.localRotation = rotation;
        dics[hashCode].Add(obj);
        return obj;
    }
}
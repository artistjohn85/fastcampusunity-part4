using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class ResourcesManager : ManagerBase
{
    private static ResourcesManager instance;
    public static ResourcesManager Instance
    {
        get
        {
            if (instance == null)
                instance = new GameObject("ResourcesManager").AddComponent<ResourcesManager>();

            return instance;
        }
    }

    private AssetBundle assetBundle;

    private void Awake()
    {
        Dontdestory<ResourcesManager>();
    }

    public void SetInit()
    {
    }

    public IEnumerator ResourcesLoad<T>(string name) where T : Object
    {
#if USE_ASSET_BUNDLE
        AssetBundleRequest assetBundleRequest = assetBundle.LoadAssetAsync(name);
        yield return assetBundleRequest;
        yield return assetBundleRequest.asset;
#else
        ResourceRequest resourceRequest = Resources.LoadAsync<T>(name);
        yield return resourceRequest;
        yield return resourceRequest.asset;
#endif
    }

    public IEnumerator AssetBundleLoad(string url, int version)
    {
#if USE_ASSET_BUNDLE_SERVER
        IEnumerator eServerLoad = DownloadAndLoadAssetBundle(url, version);
        yield return StartCoroutine(eServerLoad);
        this.assetBundle = eServerLoad.Current as AssetBundle;
#else
        IEnumerator eLocalLoad = LocalLoadAssetBundle();
        yield return StartCoroutine(eLocalLoad);
        this.assetBundle = eLocalLoad.Current as AssetBundle;
#endif
        if (assetBundle == null)
        {
            yield return false;
            yield break;
        }

        yield return true;
    }

    public IEnumerator DownloadAndLoadAssetBundle(string url, int version)
    {
        string path = Path.Combine(url, $"{Config.E_ENVIRONMENT_TYPE.ToString()}/{version}/{Config.E_OS_TYPE.ToString()}/bundle.txt");
        Debug.Log(path);
        using (UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(path))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + request.error);
                yield return null;
            }
            else
            {
                this.assetBundle = DownloadHandlerAssetBundle.GetContent(request);
                yield return this.assetBundle;
            }
        }
    }

    public IEnumerator LocalLoadAssetBundle()
    {
        AssetBundleCreateRequest assetBundleCreateRequest 
            = AssetBundle.LoadFromFileAsync($"Assets/AssetBundles/{Config.E_OS_TYPE.ToString()}/bundle");
        yield return assetBundleCreateRequest;
        yield return assetBundleCreateRequest.assetBundle;
    }
}

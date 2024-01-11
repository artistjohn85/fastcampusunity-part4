using UnityEngine;
using UnityEditor;

public class BuildAssetBundles : MonoBehaviour
{
    [MenuItem("Tools/Build AssetBundles")]
    static void BuildAllAssetBundles()
    {
#if UNITY_ANDROID
        BuildPipeline.BuildAssetBundles("Assets/AssetBundles/Android", BuildAssetBundleOptions.None, BuildTarget.Android);
#elif UNITY_IOS
    BuildPipeline.BuildAssetBundles("Assets/AssetBundles/iOS", BuildAssetBundleOptions.None, BuildTarget.StandaloneOSXUniversal);
#else
        BuildPipeline.BuildAssetBundles("Assets/AssetBundles/Windows", BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
#endif
    }
}

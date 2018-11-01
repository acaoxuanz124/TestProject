using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class AssetBundleUtil {

    [MenuItem("Assets/Create/AssetBundleBuildFolder")]
    public static void CreateAssetBundleBuildFolder()
    {
        Tool.CreateDirectory(GameConfig.AssetsFullPath + GameConfig.AssetBundleConfigPath);
        UnityEngine.Object obj = AssetDatabase.LoadAssetAtPath(GameConfig.AssetsPath + GameConfig.AssetBundleConfigPath, typeof(Object));

        EditAsset editAsset = ScriptableObject.CreateInstance<EditAsset>();
        editAsset.selectDoneEvent.AddListener(CreateAssetBundleBuildInfo);
        
        ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0, editAsset, "New AssetBundleBuildFolder.asset", Texture2D.whiteTexture, null);
    }
    public static void CreateAssetBundleBuildInfo(string assetsPath)
    {
        AssetBundleBuildInfo assetBundleBuildInfo = ScriptableObject.CreateInstance<AssetBundleBuildInfo>();

        AssetDatabase.CreateAsset(assetBundleBuildInfo, assetsPath);

        AssetDatabase.SaveAssets();
    }
}

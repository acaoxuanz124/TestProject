using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.ComponentModel;
using UnityEngine.Events;

public class ToolEditor
{
    [MenuItem("Assets/ClearCache")]
    static void ClearCache()
    {
        Caching.ClearCache();

    }
    public static string[] GetDependencies(string assetPath, bool recursive = true)
    {
        List<string> listAssetPath = new List<string>();
        listAssetPath.AddRange(AssetDatabase.GetDependencies(assetPath, recursive));
        for (int i = listAssetPath.Count - 1; i >= 0; i--)
        {
            string depenAssetPath = listAssetPath[i];
            string extension = Path.GetExtension(depenAssetPath);
            if (depenAssetPath == assetPath
                || string.IsNullOrEmpty(extension)
                || extension == ".cs"
                || extension == ".asset")
                listAssetPath.RemoveAt(i);
        }
        return listAssetPath.ToArray();
    }
    public static string OpenFilePanelLocal(string title, string defaultAssetsPath, string extension, bool isToAssetPath = false)
    {
        return OpenFilePanelFull(title, GameConfig.Asset.AssetsFullPath + defaultAssetsPath, extension, isToAssetPath);
    }
    public static string OpenFilePanelFull(string title, string defaultFullPath, string extension, bool isToAssetPath = false)
    {
        Tool.CreateDirectory(defaultFullPath);
        AssetDatabase.ImportAsset(Tool.ToAssetsPath(defaultFullPath));
        string filePath = EditorUtility.OpenFilePanel(title, defaultFullPath, extension);
        return isToAssetPath ? Tool.ToAssetsPath(filePath) : filePath;
    }

    public static string OpenFolderPanelLocal(string title, string defaultAssetsPath, string defaultName, bool isToAssetPath = false)
    {
        return OpenFolderPanelFull(title, GameConfig.Asset.AssetsFullPath + defaultAssetsPath, defaultName, isToAssetPath);
    }
    public static string OpenFolderPanelFull(string title, string defaultFullPath, string defaultName, bool isToAssetPath = false)
    {
        string filePath = EditorUtility.OpenFolderPanel(title, defaultFullPath, defaultName);
        if (isToAssetPath)
            filePath = Tool.ToAssetsPath(filePath);
        return filePath;
    }

    public static void CreateFile(string assetsPath, string defaultName, Texture2D texture, UnityAction<string> doneCreate)
    {
        Tool.CreateDirectory(GameConfig.Asset.AssetsFullPath + assetsPath);

        UnityEngine.Object obj = AssetDatabase.LoadAssetAtPath(GameConfig.Asset.AssetsPath + assetsPath, typeof(UnityEngine.Object));

        Selection.activeObject = obj;

        EditAsset editAsset = ScriptableObject.CreateInstance<EditAsset>();

        editAsset.selectDoneEvent.AddListener(doneCreate);

        ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0, editAsset, defaultName, Texture2D.whiteTexture, null);
    }
    public static void CreateScriptableObject<T>(string assetsPath, string defaultName, Texture2D texture)
        where T : ScriptableObject
    {
        CreateFile(assetsPath, defaultName + ".asset", texture, delegate (string path)
           {
                T stObject = ScriptableObject.CreateInstance<T>();
               AssetDatabase.CreateAsset(stObject, path);
               AssetDatabase.ImportAsset(path);
           });
    }
}

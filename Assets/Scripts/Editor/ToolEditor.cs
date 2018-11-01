using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.ComponentModel;

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
        return OpenFilePanelFull(title, GameConfig.AssetsFullPath +  defaultAssetsPath, extension, isToAssetPath);
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
        return OpenFolderPanelFull(title, GameConfig.AssetsFullPath + defaultAssetsPath, defaultName, isToAssetPath);
    }
    public static string OpenFolderPanelFull(string title, string defaultFullPath, string defaultName, bool isToAssetPath = false)
    {
        string filePath = EditorUtility.OpenFolderPanel(title, defaultFullPath, defaultName);
        if (isToAssetPath)
            filePath = Tool.ToAssetsPath(filePath);
        return filePath;
    }
}

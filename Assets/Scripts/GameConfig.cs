using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class GameConfig
{

    #region GameAsset


    public const string GameAssetServerIp = "http://192.168.1.108/";

    #endregion

    #region AssetConfig
    public const string assetBundleDirName = @"AssetBundles";

    public const string LuaDirName = "Lua";

    public const string depenFileName = "dep.txt";

    public const string AssetsPath = "Assets/";

    public const string AssetPath = AssetsPath + "Asset/";


    public const string AssetsConfigPath = "Config/";

    public const string AssetBundleConfigPath = AssetsConfigPath + "AssetBundle";

    public const string AssetConfigPath = AssetsConfigPath + "Asset";

    public static readonly Encoding depenFileEncoding = Encoding.UTF8;

    public static string AssetFullPath
    {
        get
        {
            return Application.dataPath + "/Asset/";
        }
    }
    public static string AssetsFullPath
    {
        get
        {
            return Application.dataPath + "/";
        }
    }
    public static string AssetBundleLoadPath
    {
        get
        {
            return Tool.AppReadPath + assetBundleDirName + "/";
        }
    }
    public static string AssetBundleSavePath
    {
        get
        {
            return Tool.AppWriteReadPath + assetBundleDirName + "/";
        }
    }
    public static string LuaLoadPath
    {
        get
        {
            return Tool.AppReadPath + LuaDirName + "/";
        }
    }
    public static string LuaSavePath
    {
        get
        {
            return Tool.AppWriteReadPath + LuaDirName + "/";
        }
    }
    public static string AssetScenePath
    {
        get
        {
            return AssetPath + "Scenes/";
        }
    }
    #endregion
}

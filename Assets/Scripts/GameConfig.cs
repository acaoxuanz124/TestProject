using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class GameConfig
{

    #region GameAsset

    public class GameUpdate
    {
        public const string GameAssetServerIp = "http://192.168.1.108/";

        public const string filesName = "files.txt";

    }


    #endregion

    #region AssetConfig
    public class Asset
    {
        public const string assetBundleDirName = @"AssetBundles";

        public const string LuaDirName = "Lua";

        public const string depenFileName = "dep.txt";

        public const string AssetsPath = "Assets/";

        public const string AssetPath = AssetsPath + "Asset/";


        public const string AssetsConfigPath = "Config/";

        public const string AssetBundleConfigPath = AssetsConfigPath + "AssetBundle";

        public const string AssetConfigPath = AssetsConfigPath + "Asset";

        public const string AssetScenePath = AssetPath + "Scenes/";

        public const string AssetUIPath = AssetPath + "UI/";

        public const string AssetUIPackagePath = AssetUIPath + "Package/";

        public const string AssetUIPackagePathFormat = AssetUIPackagePath + "{0}/{1}";

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
    }

    #endregion
}

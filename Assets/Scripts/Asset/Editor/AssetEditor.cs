using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;






public class AssetEditor : EditorWindow
{
    private const string ReadTypeEditorPath = "Asset/ReadType/Editor";
    private const string ReadTypeAssetBundlePath = "Asset/ReadType/AssetBundle";

    static int ReadType
    {
        get
        {
            return PlayerPrefs.GetInt(AssetManager.ReadTypeKey, (byte)AssetReadType.Editor);
        }
    }
    static string ShowVersion
    {
        get
        {
            return PlayerPrefs.GetString(AssetManager.ShowVersionKey, "1.0.0");
        }
    }

    static int Version
    {
        get
        {
            return PlayerPrefs.GetInt(AssetManager.VersionKey, 0);
        }
    }
    [MenuItem(ReadTypeEditorPath)]
    public static void SetReadTypeEditor()
    {
        PlayerPrefs.SetInt(AssetManager.ReadTypeKey, (int)AssetReadType.Editor);
    }
    [MenuItem(ReadTypeEditorPath, true)]
    public static bool SetReadTypeMenuEditor()
    {
        Menu.SetChecked(ReadTypeEditorPath, ReadType == (int)AssetReadType.Editor);
        return true;
    }
    [MenuItem(ReadTypeAssetBundlePath)]
    public static void SetReadTypeAssetBundle()
    {
        PlayerPrefs.SetInt(AssetManager.ReadTypeKey, (int)AssetReadType.AssetBundle);
    }
    [MenuItem(ReadTypeAssetBundlePath, true)]
    public static bool SetReadTypeMenuAssetBundle()
    {
        Menu.SetChecked(ReadTypeAssetBundlePath, ReadType == (int)AssetReadType.AssetBundle);
        return true;
    }

    [MenuItem("Asset/AssetEditor")]
    private static void OpenAssetEditor()
    {
        EditorWindow.GetWindow<AssetEditor>();
    }
    void OnGUI()
    {
        GUILayout.BeginVertical();
        {
            GUILayout.BeginHorizontal();
            {
                GUILayout.Label("ShowVersion:");
                string showVersion = GUILayout.TextField(ShowVersion);
                if (showVersion != ShowVersion)
                {
                    PlayerPrefs.SetString(AssetManager.ShowVersionKey, showVersion);
                }
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            {
                GUILayout.Label("Version:");
                string versionStr = GUILayout.TextField(Version.ToString());
                long version = Tool.StringToInt64(versionStr);
                if (version != Version)
                {
                    PlayerPrefs.SetInt(AssetManager.VersionKey, (int)version);
                }
            }
            GUILayout.EndHorizontal();
            if (GUILayout.Button("Build"))
            {
                BuildAsset();
            }
        }
        GUILayout.EndVertical();
    }

    static void BuildAsset()
    {
        EditorUtility.DisplayProgressBar("BuildAssetBundles", "BuildAssetBundles...", 0);
        AssetBundleEditor.BuildAssetBundles();
        EditorUtility.DisplayProgressBar("ExportLuaLoadPath", "ExportLuaLoadPath...", 33);
        ExportLuaLoadPath();
        EditorUtility.DisplayProgressBar("GenerateFiles", "GenerateFiles...", 66);
        GenerateFiles();
        EditorUtility.DisplayProgressBar("Done", "Done...", 100);
        EditorUtility.ClearProgressBar();
    }


    [MenuItem("Asset/Export Lua to LoadPath")]
    private static void ExportLuaLoadPath()
    {
        ToLuaMenu.CopyLuaFilesToRes();

        Tool.CreateDirectory(GameConfig.Asset.LuaLoadPath);
        List<string> listLuaFilePath = Tool.GetDirectoryFiles(LuaConst.luaDir, new string[] { ".meta" }, true, true);
        listLuaFilePath.AddRange(Tool.GetDirectoryFiles(LuaConst.toluaDir, new string[] { ".meta" }, true, true));
        for (int i = 0; i < listLuaFilePath.Count; i++)
        {
            string path = listLuaFilePath[i];
            string luaDirChildPath = string.Empty;
            if (path.StartsWith(LuaConst.luaDir))
            {
                luaDirChildPath = path.Replace(LuaConst.luaDir + "/", "");
            }
            else if (path.StartsWith(LuaConst.toluaDir))
            {
                luaDirChildPath = path.Replace(LuaConst.toluaDir + "/", "");
            }
            string loadPath = GameConfig.Asset.LuaLoadPath + luaDirChildPath;
            if (string.IsNullOrEmpty(Path.GetExtension(loadPath)))
            {
                Tool.CreateDirectory(loadPath);
                continue;
            }
            //loadPath += ".bytes";
            File.Copy(path, loadPath, true);
        }

        AssetDatabase.Refresh();
    }
    [MenuItem("Asset/GenerateFiles")]
    static void GenerateFiles()
    {
        List<string> ListFilePath = Tool.GetDirectoryFiles(Tool.AppReadPath, new string[] { ".meta", ".manifest", ".txt" }, true, true);
        for (int i = 0; i < ListFilePath.Count; i++)
            ListFilePath[i] = ListFilePath[i].Replace(Tool.AppReadPath, "");
        ListFilePath.Add(GameConfig.GameUpdate.filesName);
        string jsonStr = JsonUtility.ToJson(new AssetInfo(ShowVersion, Version, ListFilePath));
        File.WriteAllText(Tool.AppReadPath + GameConfig.GameUpdate.filesName, jsonStr, Encoding.UTF8);
        AssetDatabase.Refresh();
    }
    [MenuItem("Asset/CopyAssetToServer")]
    public static void CopyAssetToServer()
    {
        Tool.CreateDirectory(EditorConfig.GameAssetLocalPath);

        string curMaxVersionPath = EditorConfig.GameAssetLocalPath + "curMaxVersion.txt";

        int lastMaxVersion = 0;
        string lastMaxVersionStr = Tool.ReadTxt(curMaxVersionPath, Encoding.UTF8);
        if (!string.IsNullOrEmpty(lastMaxVersionStr))
            lastMaxVersion = int.Parse(lastMaxVersionStr);

        if (lastMaxVersion > Version)
        {
            EditorUtility.DisplayDialog("版本错误", "当前最新版本大于本次所提交的版本", "确认");
            return;
        }
        string assetInfoStr = Tool.ReadTxt(Tool.AppReadPath + GameConfig.GameUpdate.filesName, Encoding.UTF8);
        AssetInfo assetInfo = JsonUtility.FromJson<AssetInfo>(assetInfoStr);

        string curVersionPath = EditorConfig.GameAssetLocalPath + assetInfo.version + "/";

        Tool.SaveTxt(assetInfo.version.ToString(), curMaxVersionPath, Encoding.UTF8);

        Tool.DeleteDirectory(curVersionPath, true);

        Tool.CreateDirectory(curVersionPath);

        for (int i = 0; i < assetInfo.listFilePath.Count; i++)
        {
            string localFilePath = assetInfo.listFilePath[i];

            string filePath = Tool.AppReadPath + localFilePath;

            string copyFilePath = curVersionPath + localFilePath;

            if (string.IsNullOrEmpty(Path.GetExtension(copyFilePath)))
            {
                Tool.CreateDirectory(copyFilePath);
            }
            else
            {
                File.Copy(filePath, copyFilePath, true);
            }
        }
    }
    /// <summary>
    /// 生成对比文件
    /// </summary>
    /// <param name="lastVer">上一次版本</param>
    /// <param name="curVer">当前版本</param>
    private static bool GenerateDiffFiles(int lastVer, int curVer)
    {

        AssetInfo lastAssetInfo = GetAssetInfo(lastVer);

        AssetInfo curAssetInfo = GetAssetInfo(curVer);

        if (lastAssetInfo == null || curAssetInfo == null)
            return false;






        return true;
    }
    static AssetInfo GetAssetInfo(string path)
    {
        AssetInfo assetInfo = null;
        string assetInfoStr = Tool.ReadTxt(path, Encoding.UTF8);
        if (!string.IsNullOrEmpty(assetInfoStr))
            assetInfo = JsonUtility.FromJson<AssetInfo>(assetInfoStr);
        return assetInfo;
    }
    static AssetInfo GetAssetInfo(int ver)
    {
        return GetAssetInfo(EditorConfig.GameAssetLocalPath + ver + "/files.txt");
    }
}

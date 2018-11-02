using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;

public class AssetEditor
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
    [MenuItem("Asset/Export Lua to LoadPath")]
    public static void ExportLuaLoadPath()
    {
        Tool.CreateDirectory(GameConfig.LuaLoadPath);
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
            string loadPath = GameConfig.LuaLoadPath + luaDirChildPath;
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
        StringBuilder sb = new StringBuilder();
        List<string> ListFilePath =  Tool.GetDirectoryFiles(Tool.AppReadPath, new string[] { ".meta", ".manifest",".txt" }, true, true);
        for (int i = 0; i < ListFilePath.Count; i++)
        {
            string filePath = ListFilePath[i];
            string readLocalPath = filePath.Replace(Tool.AppReadPath,"");
            sb.AppendLine(readLocalPath);
        }
        sb.Append("files.txt");
        File.WriteAllText(Tool.AppReadPath + "files.txt", sb.ToString(),Encoding.UTF8);
        AssetDatabase.Refresh();
    }
}

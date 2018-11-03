using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public static class EditorConfig
{
    public const string GameAssetServerDir = "GameAssetServer";

    public static string GameAssetLocalPath
    {
        get
        {
            return CurrentDirectoryPath + GameAssetServerDir + "/";
        }
    }
    public static string CurrentDirectoryPath
    {
        get
        {
            return Tool.StartPath(Directory.GetCurrentDirectory()) + "/";
        }
    }
}

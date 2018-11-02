using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Text;
using System.Text.RegularExpressions;

public static class Tool
{
    #region Editor

    #endregion
    /// <summary>
    /// 网络可用
    /// </summary>
    public static bool NetAvailable
    {
        get
        {
            return Application.internetReachability != NetworkReachability.NotReachable;
        }
    }
    /// <summary>
    /// 是否是无线
    /// </summary>
    public static bool IsWifi
    {
        get
        {
            return Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork;
        }
    }
    /// <summary>
    /// 可读写目录
    /// </summary>
    public static string AppWriteReadPath
    {
        get
        {
            if (Application.isMobilePlatform)
            {
                return Application.persistentDataPath + "/";
            }
            if (Application.platform == RuntimePlatform.OSXEditor)
            {
                int i = Application.dataPath.LastIndexOf('/');
                return Application.dataPath.Substring(0, i + 1) + "/";
            }
            return StartPath(Directory.GetCurrentDirectory()) + "/persistentDataPath/";
        }
    }
    /// <summary>
    /// 可读目录
    /// </summary>
    public static string AppReadPath
    {
        get
        {
            string path = string.Empty;
            switch (Application.platform)
            {
                case RuntimePlatform.Android:
                    path = "jar:file://" + Application.dataPath + "!/assets/";
                    break;
                case RuntimePlatform.IPhonePlayer:
                    path = "file://" + Application.dataPath + "/Raw/";
                    break;
                default:
                    path = Application.streamingAssetsPath + "/";
                    break;
            }
            return path;
        }
    }

    public static string ToAssetsPath(string filePath)
    {
        if (filePath == null)
            return null;
        int assetsIndex = filePath.IndexOf("Assets/");
        if (assetsIndex == -1)
            return null;
        return filePath.Substring(assetsIndex);
    }
    public static bool IsDirectoryExists(string path)
    {
        return Directory.Exists(path);
    }
    public static void CreateDirectory(string path)
    {
        if (!IsDirectoryExists(path))
            Directory.CreateDirectory(path);
    }
    public static void DeleteDirectory(string path, bool recursive = true)
    {
        if (IsDirectoryExists(path))
            Directory.Delete(path, recursive);
    }
    public static bool IsFileExists(string path)
    {
        return File.Exists(path);
    }
    public static void DeleteFile(string path)
    {
        if (IsFileExists(path))
            File.Delete(path);
    }
    public static string StartPath(string path)
    {
        if (string.IsNullOrEmpty(path))
            return path;
        return path.Trim('﻿').Replace("\\", "/");
    }
    public static List<string> GetDirectoryFiles(string path, string[] fileNotNeedExtensions, bool isHaveDirectory, bool isFindChild, List<string> ListFilePaths = null)
    {
        if (ListFilePaths == null)
            ListFilePaths = new List<string>();
        if (string.IsNullOrEmpty(path))
            return ListFilePaths;
        DirectoryInfo dirInfo = new DirectoryInfo(path);
        FileInfo[] fileInfos = dirInfo.GetFiles();
        for (int i = 0; i < fileInfos.Length; i++)
        {
            FileInfo fileInfo = fileInfos[i];
            string fullName = StartPath(fileInfo.FullName);
            if (fileNotNeedExtensions == null)
            {
                ListFilePaths.Add(fullName);
            }
            else
            {
                bool IsNeed = true;
                for (int n = 0; n < fileNotNeedExtensions.Length; n++)
                {
                    if (fileInfo.Extension == fileNotNeedExtensions[n])
                    {
                        IsNeed = false;
                        break;
                    }
                }
                if (IsNeed)
                {
                    ListFilePaths.Add(fullName);
                }
            }
        }
        if (isHaveDirectory || isFindChild)
        {
            DirectoryInfo[] dirChildInfos = dirInfo.GetDirectories();
            for (int i = 0; i < dirChildInfos.Length; i++)
            {
                DirectoryInfo dirChildInfo = dirChildInfos[i];
                string fullName = StartPath(dirChildInfo.FullName);
                if (isHaveDirectory)
                    ListFilePaths.Add(fullName);
                if (isFindChild)
                    GetDirectoryFiles(dirChildInfo.FullName, fileNotNeedExtensions, isHaveDirectory, isFindChild, ListFilePaths);
            }
        }
        return ListFilePaths;
    }
    public static int Random(int min, int max)
    {
        return UnityEngine.Random.Range(min, max);
    }
    public static float Random(float min, float max)
    {
        return UnityEngine.Random.Range(min, max);
    }
    public static double GetUtcTime(int hour)
    {
        return Math.Round((
            DateTime.UtcNow
            .AddHours(hour) - new DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds, MidpointRounding.AwayFromZero);
    }
    public static DateTime GetUtcDateTime(int hour)
    {
        return DateTime.UtcNow.AddHours(hour);
    }
    public static string ReturnTabText(string text, Font font, int fontSize, int MaxWidth, FontStyle fontStyle)
    {
        string str = string.Empty;
        CharacterInfo chatInfo;
        int curWidth = 0;
        int lastIndex = 0;
        font.RequestCharactersInTexture(text, fontSize, fontStyle);
        for (int i = 0; i < text.Length; i++)
        {
            char textItem = text[i];
            if (font.GetCharacterInfo(textItem, out chatInfo, fontSize, fontStyle))
            {

                int toWidth = curWidth + chatInfo.advance;
                if (i == 0)
                    toWidth += chatInfo.advance / 2;
                if (toWidth > MaxWidth)
                {
                    str += text.Substring(lastIndex, i - lastIndex);
                    str += "\n" + textItem;
                    curWidth = 0;
                    lastIndex = i + 1;
                }
                else
                {
                    curWidth = toWidth;
                }
            }
        }
        str += text.Substring(lastIndex, text.Length - lastIndex);
        return str;
    }
    public static string ReturnTabText(string text, GUIStyle guiStyle, int maxWidth)
    {
        Font font = guiStyle.font;
        if (font == null)
            font = GUI.skin.font;
        return ReturnTabText(text, font, guiStyle.fontSize, maxWidth, guiStyle.fontStyle);
    }
    public static string ReadTxt(string path, Encoding encoding)
    {
        string text = "";
        if (IsFileExists(path))
            text = File.ReadAllText(path, encoding);
        return text;
    }
    public static void SaveTxt(string text, string path, Encoding encoding)
    {
        File.WriteAllText(path, text, encoding);
    }
    public static string GetReadPath(string localReadPath)
    {
        string path = AppWriteReadPath + localReadPath;
        if (!File.Exists(path))
            path = AppReadPath + localReadPath;
        return path;
    }
    public static long InputNum(string text)
    {
        long num = -1;
        text = Regex.Replace(text, "[^0-9]", "");
        if (string.IsNullOrEmpty(text))
            return num;
        num = Convert.ToInt64(text);
        return num;
    }
}

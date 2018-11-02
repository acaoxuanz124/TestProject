using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class GameUpdate
{
    static string _error;

    static UnityEngine.Events.UnityAction _doneAction;
    public static void init(UnityEngine.Events.UnityAction doneAction)
    {
        _doneAction = doneAction;
        Game.instance.StartCoroutine(_init());
    }
    private static IEnumerator _init()
    {
        yield return _Export();
        yield return _Update();
        if (_doneAction != null)
            _doneAction();
    }
    //导出
    private static IEnumerator _Export()
    {
        if (!IsExport())
            yield break;
        List<string> listPath = new List<string>();
        yield return WWWUtil.Load(Tool.AppReadPath + "files.txt", delegate (WWW www)
       {
           if (!string.IsNullOrEmpty(www.error))
           {
               Debuger.Log("GameUpdate._Export", www.error);
               return;
           }
           StringReader stringReader = new StringReader(www.text);
           while (true)
           {
               string localPath = stringReader.ReadLine();
               if (localPath == null)
                   break;
               localPath = localPath.Trim();
               listPath.Add(localPath);
           }
       });
        Tool.CreateDirectory(Tool.AppWriteReadPath);
        for (int i = 0; i < listPath.Count; i++)
        {
            string path = listPath[i];
            string fileReadPath = Tool.AppReadPath + path;
            string fileReadWritePath = Tool.AppWriteReadPath + path;
            if (string.IsNullOrEmpty(Path.GetExtension(path)))
            {
                Tool.CreateDirectory(fileReadWritePath);
            }
            else
            {
                yield return WWWUtil.Load(fileReadPath, delegate (WWW www)
                {
                    if (!string.IsNullOrEmpty(www.error))
                    {
                        _error = www.error;
                        Debuger.LogError(www.error);
                        return;
                    }
                    Debuger.Log(fileReadWritePath);
                    File.WriteAllBytes(fileReadWritePath, www.bytes);
                });
                if (_error != null)
                    break;
            }
        }
    }
    private static bool IsExport()
    {
        bool isExport = false;
        byte count = (byte)GameConfig.ExportFolderStruct.Count;
        for (int i = 0; i < count; i++)
        {
            string folderName = ((GameConfig.ExportFolderStruct)i).ToString();
            if (!Tool.IsDirectoryExists(Tool.AppWriteReadPath + folderName))
            {
                isExport = true;
                break;
            }
        }
        return isExport;


    }
    //更新
    private static IEnumerator _Update()
    {
        yield return 0;

    }

}

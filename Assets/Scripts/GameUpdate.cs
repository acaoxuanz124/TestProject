using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class GameUpdate
{
    static string error;

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
        {
            _doneAction();
        }

    }
    //导出
    private static IEnumerator _Export()
    {
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
                 listPath.Add(localPath);
             }
         });
        if (listPath.Count == 0)
        {
            error = "listPath.Count == 0";
            yield break;
        }

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
                         error = www.error;
                         return;
                     }
                     File.WriteAllBytes(fileReadWritePath, www.bytes);
                 });
                if (error != null)
                {
                    yield break;
                }
            }
        }


    }
    //更新
    private static IEnumerator _Update()
    {
        yield return 0;

    }

}

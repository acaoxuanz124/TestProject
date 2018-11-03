using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public static class GameAsset
{
    static string _error;

    static AssetInfo assetInfo;

    static UnityEngine.Events.UnityAction _doneAction;
    public static void init(UnityEngine.Events.UnityAction doneAction)
    {
        _doneAction = doneAction;
        Game.instance.StartCoroutine(_init());
    }
    private static IEnumerator _init()
    {
        yield return WWWUtil.Load(Tool.AppReadPath + "files.txt", delegate(WWW www)
        {
            if (!string.IsNullOrEmpty(www.error))
            {
                _error = www.error;
                return;
            }
            string text = www.text;
            text = text.Trim();
            assetInfo = JsonUtility.FromJson<AssetInfo>(text);
        } );
        if (_error != null)
        {
            yield break;
        }
        yield return _Export();
        if (_error != null)
        {
            yield break;
        }
        yield return _Update();
        if (_error != null)
        {
            yield break;
        }
        if (_doneAction != null)
            _doneAction();
    }
    //导出
    private static IEnumerator _Export()
    {
        if (IsExportDone())
            yield break;
        Tool.CreateDirectory(Tool.AppWriteReadPath);
        for (int i = 0; i < assetInfo.listFilePath.Count; i++)
        {
            string path = assetInfo.listFilePath[i];
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
        if (_error != null)
        {
            yield break;
        }
        Tool.CreateDirectory(Tool.AppWriteReadPath + "exportDone");
    }
    private static bool IsExportDone()
    {
        return Tool.IsDirectoryExists(Tool.AppWriteReadPath + "exportDone");
    }
    //更新
    private static IEnumerator _Update()
    {




        yield return 0;

    }

}

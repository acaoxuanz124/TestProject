using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class GameUpdate
{
    static string _error;

	static int curDoneFileCount;

    static UnityEngine.Events.UnityAction _doneAction;
    public static void init(UnityEngine.Events.UnityAction doneAction)
    {
        _doneAction = doneAction;
		_init ();
    }
	private static void _init()
    {
        _Export();

    }
    //导出
    private static void _Export()
    {
		WWWUtil.Text(Tool.AppReadPath + "files.txt",null,test);
    }
	private static void test(string error,string text)
	{
		List<string> listPath = new List<string>();
		if (!string.IsNullOrEmpty(error))
		{
			Debuger.Log("GameUpdate._Export", error);
			return;
		}
		StringReader stringReader = new StringReader(text);
		while (true)
		{
			string localPath = stringReader.ReadLine();
			if (localPath == null)
				break;
			listPath.Add(localPath);
		}
		curDoneFileCount = 0;
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
				WWWUtil.Bytes(fileReadPath, delegate(string error1, byte[] bytes) {
					test1(error1,fileReadWritePath,bytes);
				});
			}
		}

	}
	public static void test1(string error,string fileReadWritePath,byte[] bytes)
	{
		if (!string.IsNullOrEmpty(error))
		{
			_error = error;
			Debuger.LogError(error);
			return;
		}
		Debuger.Log (fileReadWritePath);
		File.WriteAllBytes(fileReadWritePath, bytes);
		++curDoneFileCount;
	}

    //更新
    private static IEnumerator _Update()
    {
        yield return 0;

    }

}

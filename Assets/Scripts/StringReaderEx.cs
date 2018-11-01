using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public abstract class StringReaderEx<T>
    where T : class, new()
{
    string path;
    Encoding encoding;
    UnityEngine.Events.UnityAction<T> readerDoneAction;
    public StringReaderEx(string path, Encoding encoding)
    {
        this.path = path;
        this.encoding = encoding;
    }
    /// <summary>
    /// 走 file 加载
    /// </summary>
    /// <returns></returns>
    public T Reader()
    {
        T readerData = null;
        if (!Tool.IsFileExists(this.path))
        {
            Debuger.LogError("file Exits is false");
        }
        else
        {
            readerData = Reader(Tool.ReadTxt(path, encoding));
        }
        return readerData;
    }
    /// <summary>
    /// 走 WWW 异步 加载
    /// </summary>
    /// <returns></returns>
    public void Reader(UnityEngine.Events.UnityAction<T> readerDoneAction)
    {
        this.readerDoneAction = readerDoneAction;
        WWWUtil.Text(this.path, encoding, WWWTextHandler);
    }
    private void WWWTextHandler(string error, string content)
    {
        if (!string.IsNullOrEmpty(error))
        {
            Debuger.LogError("WWW:" + error);
        }
        else
        {
            if (readerDoneAction != null)
                readerDoneAction(_Reader(content));
        }
    }
    public T Reader(string content)
    {
        T readerData = null;
        if (string.IsNullOrEmpty(content))
        {
            Debuger.LogError("content Is Null Or Empty");
        }
        else
        {
            readerData = _Reader(content);
        }
        return readerData;
    }
    protected abstract T _Reader(string content);
}

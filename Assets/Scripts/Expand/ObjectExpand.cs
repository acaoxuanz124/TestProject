using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ObjectExpand
{
    public static void Log(this object obj, object gameObj)
    {
        Debuger.Log(gameObj);
    }
    public static void Log(this object obj, string content)
    {
        Debuger.Log(content);
    }
    public static void Log(this object obj, string content, params object[] args)
    {
        Debuger.Log(content, args);
    }
    public static void LogError(this object obj, string content)
    {
        Debuger.LogError(content);
    }
    public static void LogError(this object obj, string content, params object[] args)
    {
        Debuger.LogError(content, args);
    }
    public static void LogWarning(this object obj, string content)
    {
        Debuger.LogWarning(content);
    }
    public static void LogWarning(this object obj, string content, params object[] args)
    {
        Debuger.LogWarning(content, args);
    }
    public static void Coroutine(this object obj, IEnumerator routine)
    {
        Game.instance.StartCoroutine(routine);
    }
}

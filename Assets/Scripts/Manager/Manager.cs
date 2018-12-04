using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public static class Manager
{
    private static Dictionary<string, BaseManager> _dicManager = new Dictionary<string, BaseManager>();

    private static List<string> _ListInit = new List<string>();

    private static bool IsInitDone = false;

    public static readonly UnityEventEx InitDone = new UnityEventEx();

    public static void Init()
    {

        AddManager<AssetManager>();
        AddManager<GameSceneManager>();
        AddManager<AssetBundleManager>(true);
        AddManager<LuaManager>(true);

        var tor = _dicManager.Values.GetEnumerator();
        while (tor.MoveNext())
            tor.Current.Init();
        if (_ListInit.Count == 0)
        {
            IsInitDone = true;
            InitDone.Invoke();
        }
    }
    public static void Reset()
    {
        var tor = _dicManager.Values.GetEnumerator();
        while (tor.MoveNext())
            tor.Current.Reset();
    }
    public static void Release()
    {
        var tor = _dicManager.Values.GetEnumerator();
        while (tor.MoveNext())
            tor.Current.Release();
    }

    public static T GetManager<T>()
        where T : BaseManager, new()
    {
        BaseManager manager = null;
        string managerName = typeof(T).Name;
        if (!_dicManager.TryGetValue(managerName, out manager))
        {
            Debuger.LogError("current in this thing managerName {0} is null", managerName);
            return null;
        }
        return manager as T;
    }
    public static BaseManager GetManager(string managerName)
    {
        BaseManager manager = null;
        if (!_dicManager.TryGetValue(managerName, out manager))
        {
            Debuger.LogError("current in this thing managerName {0} is null", managerName);
            return manager;
        }
        return manager;
    }
    public static void AddManager<T>(bool isWaitInit = false)
    where T : BaseManager, new()
    {
        BaseManager manager = null;
        string managerName = typeof(T).Name;
        if (!_dicManager.TryGetValue(managerName, out manager))
        {
            manager = new T();
            _dicManager.Add(managerName, manager);
            if (!IsInitDone && isWaitInit)
            {
                _ListInit.Add(managerName);
            }
            else if (IsInitDone)
            {
                manager.Init();
            }
        }
    }
    public static void InitUpdateHandle()
    {
        if (IsInitDone)
            return;
        IsInitDone = true;
        for (int i = 0; i < _ListInit.Count; i++)
        {
            if (!_dicManager[_ListInit[i]].isInit)
            {
                IsInitDone = false;
                break;
            }
        }
        if (IsInitDone)
            InitDone.Invoke();
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TPool<T> where T : ITobj, new()
{
    readonly static IPool<T> objectPool = new ObjectPool<T>(delegate () { return new T(); }, obj => obj.Return());
    public static T Get()
    {
        return objectPool.Get();
    }
    public static void Return(T obj)
    {
        objectPool.Return(obj);
    }

    public static void Release()
    {
        objectPool.Release();
    }
}

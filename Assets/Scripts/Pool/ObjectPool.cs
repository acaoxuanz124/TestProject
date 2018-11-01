using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> : IPool<T>
{
    Stack<T> _stackPool;

    Func<T> _createFunc;
    Action<T> _returnAction;
    public ObjectPool(Func<T> createFunc, Action<T> returnAction, int defaultSize = 0)
    {
        _stackPool = new Stack<T>();
        _createFunc = createFunc;
        _returnAction = returnAction;
        for (int i = 0; i < defaultSize; i++)
            Return(Get());
    }
    public T Get()
    {
        T obj = default(T);
        if (_stackPool.Count > 0)
        {
            obj = _stackPool.Pop();
        }
        else
        {
            obj = _createFunc();
        }
        return obj;
    }

    public void Release()
    {
        _stackPool.Clear();
        _stackPool = null;
    }

    public void Return(T obj)
    {
        if (_returnAction != null)
            _returnAction(obj);
        _stackPool.Push(obj);
    }
}

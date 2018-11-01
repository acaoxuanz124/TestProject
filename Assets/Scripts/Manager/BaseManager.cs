using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager : IObject
{
    private bool _isInit;
    public bool isInit
    {
        get
        {
            return _isInit;
        }
        protected set
        {
            _isInit = value;
            Manager.InitUpdateHandle();
        }
    }
    public virtual void Init()
    {
        _isInit = false;
    }
    public virtual void Reset()
    {

    }
    public virtual void Release()
    {

    }

}

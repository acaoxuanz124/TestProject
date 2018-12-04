using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public abstract class AssetLoader : ITobj, IObject
{

    private bool _isDone;

    public bool isDone
    {
        get
        {
            return _isDone;
        }
        protected set
        {
            if (_isDone)
                return;
            _isDone = value;
            if (_isDone)
                doneEvent.Invoke(this);
        }
    }
    public string AssetsPath
    {
        get;
        protected set;
    }
    public UnityEngine.Object mainAsset
    {
        get;
        protected set;
    }

    public UnityEventEx<AssetLoader> doneEvent
    {
        get;
        protected set;
    }

    public UnityEventEx<float> progressEvent
    {
        get;
        protected set;
    }


    public AssetLoader()
    {
        doneEvent = new UnityEventEx<AssetLoader>();
        progressEvent = new UnityEventEx<float>();

    }
    public virtual void SetLoader(string AssetsPath, UnityAction<AssetLoader> doneAction = null, UnityAction<float> progressAction = null)
    {
        this.AssetsPath = AssetsPath;
        this.doneEvent.AddListener(doneAction);
        this.progressEvent.AddListener(progressAction);
        this._isDone = false;
    }


    public abstract void LoadAsync();
    public abstract void Load();

    public virtual void Return()
    {
        this.AssetsPath = null;
        this.doneEvent.RemoveAllListeners();
        this.progressEvent.RemoveAllListeners();
        this._isDone = false;
    }
    protected void SetProgress(float progress)
    {
        progressEvent.Invoke(progress);
    }
    public virtual void Release()
    {
        
    }

}

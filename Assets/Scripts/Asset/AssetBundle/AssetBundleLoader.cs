using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.IO;

public class AssetBundleLoader : IObject, ITobj
{
    /// <summary>
    /// 是否加载完成
    /// </summary>
    /// <value>The name of the asset.</value>
    public bool isDone;
    /// <summary>
    /// assetBundle名称
    /// </summary>
    public string assetBundleName;
    /// <summary>
    /// 加载路径
    /// </summary>
    /// <value>The name of the asset.</value>
    public string readPath;
    /// <summary>
    /// 资源名称
    /// </summary>
    /// <value>The name of the asset.</value>
    public string assetName
    {
        get;
        private set;
    }
    /// <summary>
    /// assetBundle资源
    /// </summary>
    /// <value>The asset bundle.</value>
    public AssetBundle assetBundle
    {
        get;
        private set;
    }
    /// <summary>
    /// 当前完成回调事件
    /// </summary>
    /// <value>The progress event.</value>
    public UnityEventEx<AssetBundleLoader> doneEvent
    {
        get;
        private set;
    }
    /// <summary>
    /// 当前加载进度回调事件
    /// </summary>
    /// <value>The progress event.</value>
    public UnityEventEx<float> progressEvent
    {
        get;
        private set;
    }
    /// <summary>
    /// 异步线程优先级
    /// </summary>
    public int priority;
    /// <summary>
    /// 依赖资源总量
    /// </summary>
    public int depenCount;
    /// <summary>
    /// 当前依赖资源加载数量
    /// </summary>
    public int curDepenDoneCount;
    /// <summary>
    /// 当前被依赖次数
    /// </summary>
    public int curOfDepenNum;

    /// <summary>
    /// 当前依赖资源是否加载完成
    /// </summary>
    /// <value><c>true</c> if is load depen done; otherwise, <c>false</c>.</value>
    public bool isLoadDepenDone
    {
        get
        {
            return depenCount == 0 || curDepenDoneCount >= depenCount;
        }
    }

    public AssetBundleLoader()
    {
        doneEvent = new UnityEventEx<AssetBundleLoader>();
        progressEvent = new UnityEventEx<float>();
    }
    public void SetAssetBundleLoader(string assetBundleName, string readPath, UnityAction<AssetBundleLoader> doneAction = null, UnityAction<float> progressAction = null, int priority = 0)
    {
        this.assetBundleName = assetBundleName;
        this.readPath = readPath;
        this.doneEvent.AddListener(doneAction);
        this.progressEvent.AddListener(progressAction);
        this.priority = priority;
    }
    public void SetDepenInfo(int curDepenDoneCount = 0, int depenCount = 0, int curOfDepenNum = 0)
    {
        this.curDepenDoneCount = curDepenDoneCount;
        this.depenCount = depenCount;
        this.curOfDepenNum = curOfDepenNum;
    }
    public void Load()
    {
        this.Coroutine(_Load());
    }
    private IEnumerator _Load()
    {
        AssetBundleCreateRequest assetBundleAsync = AssetBundle.LoadFromFileAsync(readPath);
        assetBundleAsync.priority = priority;
        while (!assetBundleAsync.isDone)
        {
            progressEvent.Invoke(assetBundleAsync.progress);
            yield return 0;
        }
        assetBundle = assetBundleAsync.assetBundle;
        isDone = true;
        doneEvent.Invoke(this);
    }

    public void Release()
    {
        if (isDone)
        {
            assetBundle.Unload(true);
            assetBundle = null;
        }


    }
    public void AddCurDepenDone(AssetBundleLoader depenAssetBundleLoader)
    {
        ++depenAssetBundleLoader.curOfDepenNum;
        ++curDepenDoneCount;
    }

    public void Return()
    {
        doneEvent.RemoveAllListeners();
        progressEvent.RemoveAllListeners();
        priority = 0;
        depenCount = 0;
        curDepenDoneCount = 0;
        curOfDepenNum = 0;
        isDone = false;
        assetBundle = null;
    }
}

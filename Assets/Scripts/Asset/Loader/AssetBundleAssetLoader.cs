using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class AssetBundleAssetLoader : AssetLoader
{

    public string fileName;

    AssetBundleManager assetBundleManager;
    public override void SetLoader(string AssetsPath, UnityAction<AssetLoader> doneAction = null, UnityAction<float> progressAction = null)
    {
        base.SetLoader(AssetsPath, doneAction, progressAction);
        assetBundleManager = Manager.GetManager<AssetBundleManager>();
        fileName = Path.GetFileNameWithoutExtension(AssetsPath);
    }
    public override void Release()
    {
        base.Release();
        assetBundleManager.UnloadAssetBundle(AssetsPath);
    }
    public override void Return()
    {
        base.Return();
        fileName = null;
    }
    public override void Load()
    {
        assetBundleManager.LoadAssetBundle(AssetsPath, assetBundleLoader => this.Coroutine(LoadDone(assetBundleLoader)), p => progressEvent.Invoke(p));
    }
    private IEnumerator LoadDone(AssetBundleLoader assetBundleLoader)
    {
        AssetBundleRequest loadRequest = assetBundleLoader.assetBundle.LoadAssetAsync(fileName);
        while (!loadRequest.isDone)
        {
            progressEvent.Invoke(loadRequest.progress);
            yield return 0;
        }
        this.mainAsset = loadRequest.asset;
        isDone = true;
    }

}

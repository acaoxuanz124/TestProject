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
    public override void LoadAsync()
    {
        assetBundleManager.LoadAssetBundleAsync(AssetsPath, assetBundleLoader => this.Coroutine(LoadDone(assetBundleLoader)), p => SetProgress(0.5f * p));
    }
    private IEnumerator LoadDone(AssetBundleLoader assetBundleLoader)
    {
        AssetBundleRequest loadRequest = assetBundleLoader.assetBundle.LoadAssetAsync(fileName);
        while (!loadRequest.isDone)
        {
            SetProgress(0.5f + 0.5f * loadRequest.progress);
            yield return 0;
        }
        this.mainAsset = loadRequest.asset;
        isDone = true;
    }

    public override void Load()
    {
        SetProgress(0f);
        AssetBundleLoader assetBundleLoader = assetBundleManager.LoadAssetBundle(AssetsPath);
        SetProgress(0.5f);
        this.mainAsset = assetBundleLoader.assetBundle.LoadAsset(fileName);
        SetProgress(1f);
        isDone = true;
    }
}

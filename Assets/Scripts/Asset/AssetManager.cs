using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public enum AssetReadType : byte
{
    Editor = 1,
    AssetBundle,
}
public class AssetManager : BaseManager
{
    public const string ReadTypeKey = "Asset.ReadType";

    public static AssetReadType ReadType
    {
        get;
        private set;
    }
    Dictionary<string, AssetLoader> _dicAssetLoader;
    List<AssetLoader> _listIngAssetLoader;

    public override void Init()
    {
        base.Init();
#if UNITY_EDITOR
        ReadType = (AssetReadType)PlayerPrefs.GetInt(ReadTypeKey, (int)AssetReadType.Editor);
#else
        ReadType =AssetReadType.AssetBundle;
#endif
        _dicAssetLoader = new Dictionary<string, AssetLoader>();
        _listIngAssetLoader = new List<AssetLoader>();

    }
    public AssetLoader GetLoader(string AssetsPath)
    {
        AssetLoader assetLoader = null;
        if (_dicAssetLoader.TryGetValue(AssetsPath, out assetLoader))
            return assetLoader;
        assetLoader = _listIngAssetLoader.Find(v => v.AssetsPath == AssetsPath);
        if (assetLoader != null)
            return assetLoader;
        return assetLoader;
    }
    private AssetLoader CreateLoader()
    {
        AssetLoader assetLoader = null;
#if UNITY_EDITOR
        if (ReadType == AssetReadType.AssetBundle)
        {
            assetLoader = TPool<AssetBundleAssetLoader>.Get();
        }
        else
        {
            assetLoader = TPool<EditorAssetLoader>.Get();
        }
        return assetLoader;
#else
        return TPool<AssetBundleAssetLoader>.Get();
#endif

    }
    public void LoadAsset(string AssetPath, UnityAction<AssetLoader> doneAction, UnityAction<float> progressAction = null)
    {
        string AssetsPath = GameConfig.AssetPath + AssetPath;
        AssetLoader assetLoader = GetLoader(AssetsPath);
        if (assetLoader != null)
        {
            if (assetLoader.isDone)
            {
                if (progressAction != null)
                    progressAction(1f);
                if (doneAction != null)
                    doneAction(assetLoader);
            }
            else
            {
                assetLoader.doneEvent.AddListener(doneAction);
                assetLoader.progressEvent.AddListener(progressAction);
            }
            return;
        }
        assetLoader = CreateLoader();
        assetLoader.SetLoader(AssetsPath, LoadDone + doneAction, progressAction);
        assetLoader.Load();
        _listIngAssetLoader.Add(assetLoader);
    }
    private void LoadDone(AssetLoader assetLoader)
    {
        _listIngAssetLoader.Remove(assetLoader);
        _dicAssetLoader.Add(assetLoader.AssetsPath, assetLoader);
    }








}


using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.Events;
public class AssetBundleManager : BaseManager
{
    public const int MaxReadNum = 10;

    AssetBundleInfoStringReader assetBundleInfoStringReader;

    Dictionary<string, AssetBundleInfo> _dicAssetBundleInfo;

    Dictionary<string, AssetBundleLoader> _dicAssetBundleLoader;


    List<AssetBundleLoader> _listWaitAssetBundleLoader;

    List<AssetBundleLoader> _listIngAssetBundleLoader;

    List<AssetBundleLoader> _listWaitDepenAssetBundleLoader;

    public override void Init()
    {
        base.Init();
        _listIngAssetBundleLoader = new List<AssetBundleLoader>();
        _listWaitAssetBundleLoader = new List<AssetBundleLoader>();
        _dicAssetBundleLoader = new Dictionary<string, AssetBundleLoader>();
        _dicAssetBundleInfo = new Dictionary<string, AssetBundleInfo>();
        _listWaitDepenAssetBundleLoader = new List<AssetBundleLoader>();

        if (AssetManager.ReadType == AssetReadType.AssetBundle)
        {
            InitAssetBundleInfo();
            GameEvent.LateUpdate.AddListener(Update);
        }
        else
        {
            isInit = true;
        }
    }


    private void InitAssetBundleInfo()
    {

        assetBundleInfoStringReader = new AssetBundleInfoStringReader(GetReadPath(GameConfig.Asset.depenFileName), GameConfig.Asset.depenFileEncoding);
        assetBundleInfoStringReader.Reader(_InitAssetBundleInfo);
    }

    private void _InitAssetBundleInfo(List<AssetBundleInfo> listAssetBundleInfo)
    {
        Debuger.Log("_InitAssetBundleInfo");
        if (listAssetBundleInfo == null)
            return;
        for (int i = 0; i < listAssetBundleInfo.Count; i++)
        {
            AssetBundleInfo assetBundleInfo = listAssetBundleInfo[i];
            _dicAssetBundleInfo.Add(assetBundleInfo.assetBundleName, assetBundleInfo);
        }
        Debuger.Log("_InitAssetBundleInfo1");
        isInit = true;
    }

    private AssetBundleLoader GetAssetBundleLoader(string assetBundleName)
    {
        AssetBundleLoader assetBundleLoader;
        if (_dicAssetBundleLoader.TryGetValue(assetBundleName, out assetBundleLoader))
            return assetBundleLoader;
        assetBundleLoader = _listWaitDepenAssetBundleLoader.Find(loader => loader.assetBundleName == assetBundleName);
        if (assetBundleLoader != null)
            return assetBundleLoader;
        assetBundleLoader = _listIngAssetBundleLoader.Find(loader => loader.assetBundleName == assetBundleName);
        if (assetBundleLoader != null)
            return assetBundleLoader;
        assetBundleLoader = _listWaitAssetBundleLoader.Find(loader => loader.assetBundleName == assetBundleName);
        if (assetBundleLoader != null)
            return assetBundleLoader;
        return assetBundleLoader;
    }
    private AssetBundleInfo GetAssetBundleInfo(string assetBundleName)
    {
        AssetBundleInfo assetBundleInfo = null;
        if (!_dicAssetBundleInfo.TryGetValue(assetBundleName, out assetBundleInfo))
        {
            Debuger.LogError("assetBundleName: {0} is null", assetBundleName);
            return assetBundleInfo;
        }
        return assetBundleInfo;
    }

    public void LoadAssetBundleAsync(string path, UnityAction<AssetBundleLoader> doneAction = null, UnityAction<float> progressAction = null)
    {
        string assetBundleName = HashUtil.Get(path) + ".ab";
        AssetBundleLoader assetBundleLoader = GetAssetBundleLoader(assetBundleName);
        if (assetBundleLoader != null)
        {
            if (assetBundleLoader.isDone)
            {
                if (progressAction != null)
                    progressAction(1f);
                if (doneAction != null)
                    doneAction(assetBundleLoader);
                return;
            }
            else
            {
                assetBundleLoader.doneEvent.AddListener(doneAction);
                assetBundleLoader.progressEvent.AddListener(progressAction);
                return;
            }
        }
        AssetBundleInfo assetBundleInfo = GetAssetBundleInfo(assetBundleName);
        if (assetBundleInfo == null)
        {
            Debuger.LogError("assetBundleName: {0} is null", assetBundleName);
            return;
        }

        assetBundleLoader = TPool<AssetBundleLoader>.Get();
        assetBundleLoader.SetAssetBundleLoader(assetBundleName, GetReadPath(assetBundleName), LoadAssetBundleDone + doneAction, progressAction);
        if (assetBundleInfo.depenAssetBundleNames.Length > 0)//依赖资源加载完成的资源优先处理加载，别的通用走等待加载
        {
            int curDepenDoneCount = 0;
            for (int i = 0; i < assetBundleInfo.depenAssetBundleNames.Length; i++)
            {
                string depenAssetBundleName = assetBundleInfo.depenAssetBundleNames[i];
                AssetBundleLoader depenAssetBundleLoader = GetAssetBundleLoader(depenAssetBundleName);
                if (depenAssetBundleLoader == null)
                {
                    depenAssetBundleLoader = TPool<AssetBundleLoader>.Get();
                    depenAssetBundleLoader.SetAssetBundleLoader(depenAssetBundleName, GetReadPath(depenAssetBundleName), LoadAssetBundleDone);
                    _listWaitAssetBundleLoader.Add(depenAssetBundleLoader);
                }
                if (depenAssetBundleLoader.isDone)
                {
                    ++curDepenDoneCount;
                }
                else
                {
                    depenAssetBundleLoader.doneEvent.AddListener(assetBundleLoader.AddCurDepenDone);
                }
            }
            assetBundleLoader.SetDepenInfo(curDepenDoneCount, assetBundleInfo.depenAssetBundleNames.Length);
            _listWaitDepenAssetBundleLoader.Add(assetBundleLoader);
        }
        else
        {
            _listWaitAssetBundleLoader.Add(assetBundleLoader);
        }
    }
    public AssetBundleLoader LoadAssetBundle(string path)
    {
        string assetBundleName = HashUtil.Get(path) + ".ab";
        AssetBundleLoader assetBundleLoader = GetAssetBundleLoader(assetBundleName);
        if (assetBundleLoader != null)
        {
            if (assetBundleLoader.isDone)
                return assetBundleLoader;
            return null;
        }
        AssetBundleInfo assetBundleInfo = GetAssetBundleInfo(assetBundleName);
        if (assetBundleInfo == null)
        {
            Debuger.LogError("assetBundleName: {0} is null", assetBundleName);
            return null;
        }

        assetBundleLoader = TPool<AssetBundleLoader>.Get();
        assetBundleLoader.SetAssetBundleLoader(assetBundleName, GetReadPath(assetBundleName), LoadAssetBundleDone);
        if (assetBundleInfo.IsHaveDepen)
        {
            int curDepenDoneCount = 0;
            for (int i = 0; i < assetBundleInfo.DepenCount; i++)
            {
                string depenAssetBundleName = assetBundleInfo.depenAssetBundleNames[i];
                AssetBundleLoader depenAssetBundleLoader = GetAssetBundleLoader(depenAssetBundleName);
                if (depenAssetBundleLoader == null)
                {
                    depenAssetBundleLoader = TPool<AssetBundleLoader>.Get();
                    depenAssetBundleLoader.SetAssetBundleLoader(depenAssetBundleName, GetReadPath(depenAssetBundleName), LoadAssetBundleDone);
                    depenAssetBundleLoader.LoadAsync();
                }
                else if (depenAssetBundleLoader.isDone)
                {
                    curDepenDoneCount += 1;
                }
            }
            if (curDepenDoneCount < assetBundleInfo.depenAssetBundleNames.Length)
                return null;
        }
        assetBundleLoader.Load();
        return assetBundleLoader;
    }
    private static string GetReadPath(string name)
    {
        return Tool.GetReadPath(GameConfig.Asset.assetBundleDirName + "/" + name);
    }
    public void UnloadAssetBundle(string path)
    {
        string assetBundleName = HashUtil.Get(path) + ".ab";
        AssetBundleLoader assetBundleLoader = GetAssetBundleLoader(assetBundleName);
        if (assetBundleLoader != null && assetBundleLoader.isDone)
        {
            List<AssetBundleLoader> listUnloadDepenLoader = new List<AssetBundleLoader>();

            listUnloadDepenLoader.Add(assetBundleLoader);

            AssetBundleInfo assetBundleInfo = GetAssetBundleInfo(assetBundleName);
            for (int i = 0; i < assetBundleInfo.depenAssetBundleNames.Length; i++)
            {
                AssetBundleLoader depenAssertBundleLoader = GetAssetBundleLoader(assetBundleInfo.depenAssetBundleNames[i]);
                --depenAssertBundleLoader.curOfDepenNum;
                if (depenAssertBundleLoader.curOfDepenNum <= 0)
                    listUnloadDepenLoader.Add(depenAssertBundleLoader);
            }
            for (int i = 0; i < listUnloadDepenLoader.Count; i++)
            {
                AssetBundleLoader UnloadDepenLoader = listUnloadDepenLoader[i];
                UnloadDepenLoader.Release();
                _dicAssetBundleLoader.Remove(UnloadDepenLoader.assetBundleName);
                TPool<AssetBundleLoader>.Return(UnloadDepenLoader);
            }
        }
    }
    private void LoadAssetBundleDone(AssetBundleLoader assetBundleLoader)
    {
        _listIngAssetBundleLoader.Remove(assetBundleLoader);
        _dicAssetBundleLoader.Add(assetBundleLoader.assetBundleName, assetBundleLoader);
    }
    private void Update()
    {
        if (_listIngAssetBundleLoader.Count < MaxReadNum)
        {
            int loadNum = MaxReadNum - _listIngAssetBundleLoader.Count;
            for (int i = _listWaitDepenAssetBundleLoader.Count - 1; i >= 0; i--)
            {
                if (loadNum <= 0)
                    break;
                AssetBundleLoader assetBundleLoader = _listWaitDepenAssetBundleLoader[i];
                if (assetBundleLoader.isLoadDepenDone)
                {
                    _listWaitDepenAssetBundleLoader.RemoveAt(i);
                    _listIngAssetBundleLoader.Add(assetBundleLoader);
                    --loadNum;
                    assetBundleLoader.LoadAsync();
                }
            }
            for (int i = 0; i < loadNum; i++)
            {
                if (_listWaitAssetBundleLoader.Count <= 0)
                    break;
                AssetBundleLoader assetBundleLoader = _listWaitAssetBundleLoader[0];
                _listWaitAssetBundleLoader.RemoveAt(0);
                _listIngAssetBundleLoader.Add(assetBundleLoader);
                assetBundleLoader.LoadAsync();
            }

        }
    }
    public override void Release()
    {
        base.Release();
        if (_dicAssetBundleLoader != null)
        {
            foreach (var item in _dicAssetBundleLoader.Values)
                item.Release();
            _dicAssetBundleLoader.Clear();
        }
        if (_dicAssetBundleInfo != null)
            _dicAssetBundleInfo.Clear();

        if (_listIngAssetBundleLoader != null)
            _listIngAssetBundleLoader.Clear();

        if (_listWaitAssetBundleLoader != null)
            _listWaitAssetBundleLoader.Clear();

        if (_listWaitDepenAssetBundleLoader != null)
            _listWaitDepenAssetBundleLoader.Clear();
    }

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameSceneManager : BaseManager
{
    AssetBundleManager assetBundleManager;

    

    string curSceneName,curSceneAssetPath;
    string LastSceneName,LastSceneAssetPath;
    UnityAction doneAction;
    public override void Init()
    {
        base.Init();
        assetBundleManager = Manager.GetManager<AssetBundleManager>();
        SceneManager.sceneLoaded += SceneLoaded;
        SceneManager.sceneUnloaded += SceneUnloaded;
    }

    private void SceneUnloaded(Scene scene)
    {
        this.Log("SceneUnloaded:" + scene.name);

    }

    private void SceneLoaded(Scene scene, LoadSceneMode loadSecneMode)
    {
        this.Log("SceneLoaded:" + scene.name);
    }

    public void LoadScene(string sceneName, UnityAction doneAction = null)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            this.Log("GameSceneManager.LoadScene In this thing the SceneName is null");
            return;
        }
        if (sceneName == curSceneName)
        {
            this.Log("current scene already is : {0} scene",sceneName);
            return;
        }
        curSceneName = sceneName;
        curSceneAssetPath = GameConfig.Asset.AssetScenePath + curSceneName + ".unity";
        this.doneAction = doneAction;


        if (AssetManager.ReadType == AssetReadType.AssetBundle)
        {
            assetBundleManager.LoadAssetBundleAsync(curSceneAssetPath, assetBundleLoader => this.Coroutine(_LoadScene()));
        }
        else
        {
            this.Coroutine(_LoadScene());
        }
    }
    private IEnumerator _LoadScene()
    {
         AsyncOperation async = SceneManager.LoadSceneAsync(curSceneName);
        yield return async;


        if (!string.IsNullOrEmpty(LastSceneName))
        {
            if (AssetManager.ReadType == AssetReadType.AssetBundle)
                assetBundleManager.UnloadAssetBundle(LastSceneAssetPath);


            LastSceneName = LastSceneAssetPath = null;
        }

        LastSceneName = curSceneName;
        LastSceneAssetPath = curSceneAssetPath;
        if (doneAction != null)
            doneAction();
    }

}

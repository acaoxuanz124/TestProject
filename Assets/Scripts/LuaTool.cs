using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 用于lua与c# 交互
public static class LuaTool {
    public static GameSceneManager GetGameSceneManager()
    {
        return Manager.GetManager<GameSceneManager>();
    }
    public static AssetManager GetAssetManager()
    {
        return Manager.GetManager<AssetManager>();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;
public class EditorAssetLoader : AssetLoader
{
    public override void Load()
    {
        SetProgress(0);
        this.mainAsset = AssetDatabase.LoadMainAssetAtPath(AssetsPath);
        SetProgress(1f);
        this.isDone = true;
    }
}
#endif


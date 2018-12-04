using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetBundleInfo
{
    public string assetPath;
    public string assetBundleName;
    public string[] depenAssetBundleNames;

    public bool IsHaveDepen
    {
        get
        {
            return DepenCount > 0;
        }
    }
    public int DepenCount
    {
        get
        {
            return depenAssetBundleNames.Length;
        }
    }


}

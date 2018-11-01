using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class AssetBundleInfoStringReader : StringReaderEx<List<AssetBundleInfo>>
{
    public AssetBundleInfoStringReader(string path, Encoding encoding)
        : base(path, encoding)
    {

    }
    protected override List<AssetBundleInfo> _Reader(string content)
    {
        List<AssetBundleInfo> ListAssetBundleInfo = new List<AssetBundleInfo>();

        StringReader sr = new StringReader(content);
        while (true)
        {
            AssetBundleInfo assetBundleInfo = new AssetBundleInfo();
            assetBundleInfo.assetPath = sr.ReadLine();
            assetBundleInfo.assetBundleName = sr.ReadLine();
            assetBundleInfo.depenAssetBundleNames = new string[int.Parse(sr.ReadLine())];
            for (int i = 0; i < assetBundleInfo.depenAssetBundleNames.Length; i++)
                assetBundleInfo.depenAssetBundleNames[i] = sr.ReadLine();
            ListAssetBundleInfo.Add(assetBundleInfo);
            if (string.IsNullOrEmpty(sr.ReadLine()))
                break;
        }
        return ListAssetBundleInfo;
    }
}

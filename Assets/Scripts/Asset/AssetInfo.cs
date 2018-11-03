using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[Serializable]
public class AssetInfo
{
    public string showVersion;
    public int version;
    public List<string> listFilePath;
    public AssetInfo(string showVersion,int version,List<string> listFliePath)
    {
        this.showVersion = showVersion;
        this.version = version;
        this.listFilePath = listFliePath;
    }

}

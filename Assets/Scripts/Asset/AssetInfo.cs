using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AssetFileInfo
{
    public string filePath;
    public string md5;
    public AssetFileInfo(string filePath,string md5)
    {
        this.filePath = filePath;
        new FileInfo(filePath);
        this.md5 = md5;
    }
}

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

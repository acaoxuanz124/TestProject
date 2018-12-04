using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //File.WriteAllBytes(Tool.StartPath ("D:/UnityDomo/TestProject1/Assets/Tests/test.ab"),File.ReadAllBytes (Tool.StartPath (@"D:\UnityDomo\TestProject1\Assets\StreamingAssets\AssetBundles\0b1a8e01ecc86e5c2dbac03a062fd2f1f484a7a6.ab")));
        transform.position.Set(1, 1, 1);


    }

	// Update is called once per frame
	void Update () {
	}
    private void OnGUI()
    {
        Debuger.OnGUI();
    }
}

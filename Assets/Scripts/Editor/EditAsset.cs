
using System;
using System.IO;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using UnityEngine.Events;




public class EditAsset : EndNameEditAction
{
    public readonly UnityEventEx<string> selectDoneEvent = new UnityEventEx<string>();
    private EditAsset()
    {
    }
    public override void Action(int instanceId, string pathName, string resourceFile)
    {
        selectDoneEvent.Invoke(pathName);

        UnityEngine.Object obj = AssetDatabase.LoadAssetAtPath(pathName, typeof(UnityEngine.Object));

        ProjectWindowUtil.ShowCreatedAsset(obj);
    }

}


using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Text;
public class AssetBundleEditor : EditorWindow
{
    const int width = 600, height = 450;
    public static readonly Rect AssetBundleEditorRect = new Rect() { width = width, height = height, x = Screen.width * 0.5f - width * 0.5f, y = Screen.height * 0.5f - height * 0.5f };

    private AssetBundleBuildInfo assetBundleBuildInfo;

    private int assetBundleBuildIndex = 0;

    [MenuItem("Asset/AssetBundle/Editor")]
    static void OpenAssetBundleEditor()
    {
        GetWindowWithRect<AssetBundleEditor>(AssetBundleEditorRect);
    }
    private void OnGUI()
    {
        Repaint();
        EditorGUILayout.BeginVertical();
        {
            EditorGUILayout.BeginHorizontal();
            {
                assetBundleBuildInfo = (AssetBundleBuildInfo)EditorGUILayout.ObjectField(assetBundleBuildInfo, typeof(AssetBundleBuildInfo), false, GUILayout.Width(400));
                if (GUILayout.Button("Create"))
                {
                    AssetBundleUtil.CreateAssetBundleBuildFolder();
                }
                if (GUILayout.Button("Select"))
                {
                    ToolEditor.OpenFilePanelLocal("Overwrite with png", "/Config/AssetBundle", "asset");
                }
            }
            EditorGUILayout.EndHorizontal();

            if (assetBundleBuildInfo)
            {
                EditorGUILayout.BeginHorizontal();
                {
                    if (GUILayout.Button("Add", GUILayout.Width(100)))
                    {
                        assetBundleBuildInfo.ListAssetsPath.Add(string.Empty);
                    }
                    if (GUILayout.Button("Delete", GUILayout.Width(100)))
                    {
                        assetBundleBuildInfo.ListAssetsPath.RemoveAt(assetBundleBuildIndex);
                    }
                }
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
                {
                    EditorGUILayout.LabelField("Version:", GUILayout.Width(50)); 
                }
                EditorGUILayout.EndHorizontal();

                for (int i = 0; i < assetBundleBuildInfo.ListAssetsPath.Count; i++)
                {
                    string AssetsPath = assetBundleBuildInfo.ListAssetsPath[i];
                    EditorGUILayout.BeginHorizontal();
                    {
                        if (EditorGUILayout.ToggleLeft(AssetsPath, assetBundleBuildIndex == i, GUILayout.Width(500)))
                        {
                            assetBundleBuildIndex = i;
                        }
                        if (GUILayout.Button("select"))
                        {
                            assetBundleBuildInfo.ListAssetsPath[i] = ToolEditor.OpenFolderPanelLocal("Overwrite with png", "", "", true);
                        }
                    }
                    EditorGUILayout.EndHorizontal();
                }
            }
        }
        EditorGUILayout.EndVertical();
    }




    [MenuItem("Asset/AssetBundle/BuildAssetBundle")]
    static void BuildAssetBundles()
    {
        Dictionary<string, string> dicFileHashPath = new Dictionary<string, string>();
        List<string> listFileFullPath = Tool.GetDirectoryFiles(GameConfig.AssetFullPath, new string[] { ".meta", ".asset" }, false, true);
        List<AssetBundleBuild> listAssetBundleBuild = new List<AssetBundleBuild>();
        for (int i = 0; i < listFileFullPath.Count; i++)
        {
            string fileFullPath = listFileFullPath[i];
            string fileAssetPath = Tool.ToAssetsPath(fileFullPath);
            string hashName = HashUtil.Get(fileAssetPath) + ".ab";
            if (!dicFileHashPath.ContainsKey(hashName))
            {
                dicFileHashPath.Add(hashName, fileAssetPath);
                listAssetBundleBuild.Add(CreateAssetBundleBuild(hashName, fileAssetPath));
            }
            string[] depenFileAssetPaths = ToolEditor.GetDependencies(fileAssetPath);
            for (int n = 0; n < depenFileAssetPaths.Length; n++)
            {
                string depenFileAssetPath = depenFileAssetPaths[n];
                string depenHashName = HashUtil.Get(depenFileAssetPath) + ".ab"; ;
                if (!dicFileHashPath.ContainsKey(depenHashName))
                {
                    dicFileHashPath.Add(depenHashName, depenFileAssetPath);
                    listAssetBundleBuild.Add(CreateAssetBundleBuild(depenHashName, depenFileAssetPath));
                }

            }
        }
        Tool.CreateDirectory(Application.streamingAssetsPath);
        Tool.CreateDirectory(GameConfig.AssetBundleLoadPath);
        BuildPipeline.BuildAssetBundles(GameConfig.AssetBundleLoadPath, listAssetBundleBuild.ToArray(), BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.Android);
        AssetDatabase.Refresh();

        AssetBundle rootAssetBundle = AssetBundle.LoadFromFile(GameConfig.AssetBundleLoadPath + GameConfig.assetBundleDirName);
        AssetBundleManifest abManifest = rootAssetBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");

        StringBuilder sb = new StringBuilder();
        string[] assetBundleNames = abManifest.GetAllAssetBundles();
        for (int i = 0; i < assetBundleNames.Length; i++)
        {
            string assetBundleName = assetBundleNames[i];
            string fileAssetPath = dicFileHashPath[assetBundleName];
            string[] depenAssetBundleNames = abManifest.GetAllDependencies(assetBundleName);
            sb.AppendLine(fileAssetPath);
            sb.AppendLine(assetBundleName);
            sb.AppendLine(depenAssetBundleNames.Length.ToString());
            for (int n = 0; n < depenAssetBundleNames.Length; n++)
            {
                string depenAssetBundleName = depenAssetBundleNames[n];
                sb.AppendLine(depenAssetBundleName);
            }
            if (i != assetBundleNames.Length - 1)
            {
                sb.AppendLine("===========");
            }
        }
        rootAssetBundle.Unload(true);
        Tool.SaveTxt(sb.ToString(), GameConfig.AssetBundleLoadPath + GameConfig.depenFileName, Encoding.UTF8);

        List<string> listDeleteFilePath = new List<string>();
        listDeleteFilePath.Add(GameConfig.AssetBundleLoadPath + GameConfig.assetBundleDirName);
        listDeleteFilePath.Add(GameConfig.AssetBundleLoadPath + GameConfig.assetBundleDirName + ".manifest");
        for (int i = listDeleteFilePath.Count - 1; i >= 0; i--)
        {
            string deleteFilePath = listDeleteFilePath[i];
            Tool.DeleteFile(deleteFilePath);
        }
        AssetDatabase.Refresh();
    }
    [MenuItem("Asset/AssetBundle/ShowHash")]
    static void ShowHash()
    {
        Debuger.Log(HashUtil.Get(AssetDatabase.GetAssetPath(Selection.activeObject)));
    }
    static AssetBundleBuild CreateAssetBundleBuild(string assetBundleName, string fileAssetPath)
    {
        AssetBundleBuild assetBundleBuild = new AssetBundleBuild();
        assetBundleBuild.assetBundleName = assetBundleName;
        assetBundleBuild.assetNames = new string[] { fileAssetPath };
        return assetBundleBuild;
    }

}

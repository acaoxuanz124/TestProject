
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game instance
    {
        private set;
        get;
    }
    GameSceneManager gameSceneManager;
    AssetManager assetManager;
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
         FairyGUI.Stage stage = FairyGUI.Stage.inst;
        Debuger.Init();
    }
    // Use this for initialization
    void Start()
    {
        Manager.InitDone.AddListener(InitDone);
        Manager.Init();
    }
    void InitDone()
    {
        Debuger.Log("InitDone");
        gameSceneManager = Manager.GetManager<GameSceneManager>();
        assetManager = Manager.GetManager<AssetManager>();

        assetManager.LoadAsset("Prefabs/Cube.prefab",delegate(AssetLoader assetLoader) {
            Instantiate(assetLoader.mainAsset);



        });


    }
    // Update is called once per frame
    void Update()
    {
        GameEvent.Update.Invoke();
    }
    private void LateUpdate()
    {
        GameEvent.LateUpdate.Invoke();
    }
    private void OnGUI()
    {
        GameEvent.OnGUI.Invoke();
        if (GUI.Button(new Rect(300, 300, 200, 100), "testScene1"))
        {
            gameSceneManager.LoadScene("testScene1", delegate ()
             {
                 this.Log("testScene1 Done");
             });
        }
        if (GUI.Button(new Rect(600, 300, 200, 100), "testScene2"))
        {
            gameSceneManager.LoadScene("testScene2", delegate ()
            {
                this.Log("testScene2 Done");
            });
        }
    }
    private void OnDestroy()
    {
        Manager.Release();
    }
}

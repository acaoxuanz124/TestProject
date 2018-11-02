using LuaInterface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuaManager : BaseManager
{
    LuaState mainLuaState;
    public override void Init()
    {

        base.Init();
        Game.instance.gameObject.AddComponent<LuaClient>(); 
        mainLuaState = LuaClient.GetMainState();
        mainLuaState.DoFile("Game");
        mainLuaState.Call("Game.Start", false);
    }
}

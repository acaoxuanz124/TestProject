using LuaInterface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuaManager : BaseManager
{
    public override void Init()
    {
        base.Init();
        this.Coroutine(_Init());

    }
    private IEnumerator _Init()
    {
        Game.instance.gameObject.AddComponent<LuaClient>();
        yield return 0;
        isInit = true;
    }
    public void DoFile(string name)
    {
        LuaState luaState = LuaClient.GetMainState();
        luaState.DoFile(name);
    }
    public void Call(string name)
    {
        LuaState luaState = LuaClient.GetMainState();
        LuaFunction luaFunc = luaState.GetFunction(name);
        luaFunc.Call();
        luaFunc.Dispose();
    }
    public void LuaGC()
    {
        LuaState luaState = LuaClient.GetMainState();
        luaState.LuaGC(LuaGCOptions.LUA_GCCOLLECT, 0);
    }

}

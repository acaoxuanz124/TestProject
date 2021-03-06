﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class GameSceneManagerWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(GameSceneManager), typeof(BaseManager));
		L.RegFunction("Init", Init);
		L.RegFunction("LoadScene", LoadScene);
		L.RegFunction("New", _CreateGameSceneManager);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateGameSceneManager(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 0)
			{
				GameSceneManager obj = new GameSceneManager();
				ToLua.PushObject(L, obj);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to ctor method: GameSceneManager.New");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Init(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			GameSceneManager obj = (GameSceneManager)ToLua.CheckObject<GameSceneManager>(L, 1);
			obj.Init();
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LoadScene(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2)
			{
				GameSceneManager obj = (GameSceneManager)ToLua.CheckObject<GameSceneManager>(L, 1);
				string arg0 = ToLua.CheckString(L, 2);
				obj.LoadScene(arg0);
				return 0;
			}
			else if (count == 3)
			{
				GameSceneManager obj = (GameSceneManager)ToLua.CheckObject<GameSceneManager>(L, 1);
				string arg0 = ToLua.CheckString(L, 2);
				UnityEngine.Events.UnityAction arg1 = (UnityEngine.Events.UnityAction)ToLua.CheckDelegate<UnityEngine.Events.UnityAction>(L, 3);
				obj.LoadScene(arg0, arg1);
				return 0;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: GameSceneManager.LoadScene");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}
}


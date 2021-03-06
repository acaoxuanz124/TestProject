﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class LuaToolWrap
{
	public static void Register(LuaState L)
	{
		L.BeginStaticLibs("LuaTool");
		L.RegFunction("GetGameSceneManager", GetGameSceneManager);
		L.RegFunction("GetAssetManager", GetAssetManager);
		L.EndStaticLibs();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGameSceneManager(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 0);
			GameSceneManager o = LuaTool.GetGameSceneManager();
			ToLua.PushObject(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetAssetManager(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 0);
			AssetManager o = LuaTool.GetAssetManager();
			ToLua.PushObject(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}
}


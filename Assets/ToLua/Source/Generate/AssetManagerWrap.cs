﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class AssetManagerWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(AssetManager), typeof(BaseManager));
		L.RegFunction("Init", Init);
		L.RegFunction("LoadAsset", LoadAsset);
		L.RegFunction("New", _CreateAssetManager);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("ReadTypeKey", get_ReadTypeKey, null);
		L.RegVar("ShowVersionKey", get_ShowVersionKey, null);
		L.RegVar("VersionKey", get_VersionKey, null);
		L.RegVar("ReadType", get_ReadType, null);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateAssetManager(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 0)
			{
				AssetManager obj = new AssetManager();
				ToLua.PushObject(L, obj);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to ctor method: AssetManager.New");
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
			AssetManager obj = (AssetManager)ToLua.CheckObject<AssetManager>(L, 1);
			obj.Init();
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LoadAsset(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 3)
			{
				AssetManager obj = (AssetManager)ToLua.CheckObject<AssetManager>(L, 1);
				string arg0 = ToLua.CheckString(L, 2);
				UnityEngine.Events.UnityAction<AssetLoader> arg1 = (UnityEngine.Events.UnityAction<AssetLoader>)ToLua.CheckDelegate<UnityEngine.Events.UnityAction<AssetLoader>>(L, 3);
				obj.LoadAssetAsync(arg0, arg1);
				return 0;
			}
			else if (count == 4)
			{
				AssetManager obj = (AssetManager)ToLua.CheckObject<AssetManager>(L, 1);
				string arg0 = ToLua.CheckString(L, 2);
				UnityEngine.Events.UnityAction<AssetLoader> arg1 = (UnityEngine.Events.UnityAction<AssetLoader>)ToLua.CheckDelegate<UnityEngine.Events.UnityAction<AssetLoader>>(L, 3);
				UnityEngine.Events.UnityAction<float> arg2 = (UnityEngine.Events.UnityAction<float>)ToLua.CheckDelegate<UnityEngine.Events.UnityAction<float>>(L, 4);
				obj.LoadAssetAsync(arg0, arg1, arg2);
				return 0;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: AssetManager.LoadAsset");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_ReadTypeKey(IntPtr L)
	{
		try
		{
			LuaDLL.lua_pushstring(L, AssetManager.ReadTypeKey);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_ShowVersionKey(IntPtr L)
	{
		try
		{
			LuaDLL.lua_pushstring(L, AssetManager.ShowVersionKey);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_VersionKey(IntPtr L)
	{
		try
		{
			LuaDLL.lua_pushstring(L, AssetManager.VersionKey);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_ReadType(IntPtr L)
	{
		try
		{
			ToLua.Push(L, AssetManager.ReadType);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}
}


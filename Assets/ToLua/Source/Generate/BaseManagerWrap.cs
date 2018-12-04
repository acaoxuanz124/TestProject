﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class BaseManagerWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(BaseManager), typeof(System.Object));
		L.RegFunction("Init", Init);
		L.RegFunction("Reset", Reset);
		L.RegFunction("Release", Release);
		L.RegFunction("New", _CreateBaseManager);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("isInit", get_isInit, null);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateBaseManager(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 0)
			{
				BaseManager obj = new BaseManager();
				ToLua.PushObject(L, obj);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to ctor method: BaseManager.New");
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
			BaseManager obj = (BaseManager)ToLua.CheckObject<BaseManager>(L, 1);
			obj.Init();
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Reset(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			BaseManager obj = (BaseManager)ToLua.CheckObject<BaseManager>(L, 1);
			obj.Reset();
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Release(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			BaseManager obj = (BaseManager)ToLua.CheckObject<BaseManager>(L, 1);
			obj.Release();
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_isInit(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			BaseManager obj = (BaseManager)o;
			bool ret = obj.isInit;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index isInit on a nil value");
		}
	}
}


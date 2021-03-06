﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class ToolWrap
{
	public static void Register(LuaState L)
	{
		L.BeginStaticLibs("Tool");
		L.RegFunction("ToAssetsPath", ToAssetsPath);
		L.RegFunction("IsDirectoryExists", IsDirectoryExists);
		L.RegFunction("CreateDirectory", CreateDirectory);
		L.RegFunction("DeleteDirectory", DeleteDirectory);
		L.RegFunction("IsFileExists", IsFileExists);
		L.RegFunction("DeleteFile", DeleteFile);
		L.RegFunction("StartPath", StartPath);
		L.RegFunction("GetDirectoryFiles", GetDirectoryFiles);
		L.RegFunction("Random", Random);
		L.RegFunction("GetUtcTime", GetUtcTime);
		L.RegFunction("GetUtcDateTime", GetUtcDateTime);
		L.RegFunction("ReturnTabText", ReturnTabText);
		L.RegFunction("ReadTxt", ReadTxt);
		L.RegFunction("SaveTxt", SaveTxt);
		L.RegFunction("GetReadPath", GetReadPath);
		L.RegFunction("StringToInt64", StringToInt64);
		L.RegFunction("GetAddComponent", GetAddComponent);
		L.RegFunction("GetComponent", GetComponent);
		L.RegVar("NetAvailable", get_NetAvailable, null);
		L.RegVar("IsWifi", get_IsWifi, null);
		L.RegVar("AppWriteReadPath", get_AppWriteReadPath, null);
		L.RegVar("AppReadPath", get_AppReadPath, null);
		L.EndStaticLibs();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ToAssetsPath(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			string arg0 = ToLua.CheckString(L, 1);
			string o = Tool.ToAssetsPath(arg0);
			LuaDLL.lua_pushstring(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsDirectoryExists(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			string arg0 = ToLua.CheckString(L, 1);
			bool o = Tool.IsDirectoryExists(arg0);
			LuaDLL.lua_pushboolean(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CreateDirectory(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			string arg0 = ToLua.CheckString(L, 1);
			Tool.CreateDirectory(arg0);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DeleteDirectory(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 1)
			{
				string arg0 = ToLua.CheckString(L, 1);
				Tool.DeleteDirectory(arg0);
				return 0;
			}
			else if (count == 2)
			{
				string arg0 = ToLua.CheckString(L, 1);
				bool arg1 = LuaDLL.luaL_checkboolean(L, 2);
				Tool.DeleteDirectory(arg0, arg1);
				return 0;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: Tool.DeleteDirectory");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsFileExists(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			string arg0 = ToLua.CheckString(L, 1);
			bool o = Tool.IsFileExists(arg0);
			LuaDLL.lua_pushboolean(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DeleteFile(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			string arg0 = ToLua.CheckString(L, 1);
			Tool.DeleteFile(arg0);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int StartPath(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			string arg0 = ToLua.CheckString(L, 1);
			string o = Tool.StartPath(arg0);
			LuaDLL.lua_pushstring(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetDirectoryFiles(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 4)
			{
				string arg0 = ToLua.CheckString(L, 1);
				string[] arg1 = ToLua.CheckStringArray(L, 2);
				bool arg2 = LuaDLL.luaL_checkboolean(L, 3);
				bool arg3 = LuaDLL.luaL_checkboolean(L, 4);
				System.Collections.Generic.List<string> o = Tool.GetDirectoryFiles(arg0, arg1, arg2, arg3);
				ToLua.PushSealed(L, o);
				return 1;
			}
			else if (count == 5)
			{
				string arg0 = ToLua.CheckString(L, 1);
				string[] arg1 = ToLua.CheckStringArray(L, 2);
				bool arg2 = LuaDLL.luaL_checkboolean(L, 3);
				bool arg3 = LuaDLL.luaL_checkboolean(L, 4);
				System.Collections.Generic.List<string> arg4 = (System.Collections.Generic.List<string>)ToLua.CheckObject(L, 5, typeof(System.Collections.Generic.List<string>));
				System.Collections.Generic.List<string> o = Tool.GetDirectoryFiles(arg0, arg1, arg2, arg3, arg4);
				ToLua.PushSealed(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: Tool.GetDirectoryFiles");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Random(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 1);
			float arg1 = (float)LuaDLL.luaL_checknumber(L, 2);
			float o = Tool.Random(arg0, arg1);
			LuaDLL.lua_pushnumber(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetUtcTime(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 1);
			double o = Tool.GetUtcTime(arg0);
			LuaDLL.lua_pushnumber(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetUtcDateTime(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 1);
			System.DateTime o = Tool.GetUtcDateTime(arg0);
			ToLua.PushValue(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReturnTabText(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 3)
			{
				string arg0 = ToLua.CheckString(L, 1);
				UnityEngine.GUIStyle arg1 = (UnityEngine.GUIStyle)ToLua.CheckObject(L, 2, typeof(UnityEngine.GUIStyle));
				int arg2 = (int)LuaDLL.luaL_checknumber(L, 3);
				string o = Tool.ReturnTabText(arg0, arg1, arg2);
				LuaDLL.lua_pushstring(L, o);
				return 1;
			}
			else if (count == 5)
			{
				string arg0 = ToLua.CheckString(L, 1);
				UnityEngine.Font arg1 = (UnityEngine.Font)ToLua.CheckObject(L, 2, typeof(UnityEngine.Font));
				int arg2 = (int)LuaDLL.luaL_checknumber(L, 3);
				int arg3 = (int)LuaDLL.luaL_checknumber(L, 4);
				UnityEngine.FontStyle arg4 = (UnityEngine.FontStyle)ToLua.CheckObject(L, 5, typeof(UnityEngine.FontStyle));
				string o = Tool.ReturnTabText(arg0, arg1, arg2, arg3, arg4);
				LuaDLL.lua_pushstring(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: Tool.ReturnTabText");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadTxt(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			string arg0 = ToLua.CheckString(L, 1);
			System.Text.Encoding arg1 = (System.Text.Encoding)ToLua.CheckObject<System.Text.Encoding>(L, 2);
			string o = Tool.ReadTxt(arg0, arg1);
			LuaDLL.lua_pushstring(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SaveTxt(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			string arg0 = ToLua.CheckString(L, 1);
			string arg1 = ToLua.CheckString(L, 2);
			System.Text.Encoding arg2 = (System.Text.Encoding)ToLua.CheckObject<System.Text.Encoding>(L, 3);
			Tool.SaveTxt(arg0, arg1, arg2);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetReadPath(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			string arg0 = ToLua.CheckString(L, 1);
			string o = Tool.GetReadPath(arg0);
			LuaDLL.lua_pushstring(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int StringToInt64(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			string arg0 = ToLua.CheckString(L, 1);
			long o = Tool.StringToInt64(arg0);
			LuaDLL.tolua_pushint64(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetAddComponent(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			UnityEngine.GameObject arg0 = (UnityEngine.GameObject)ToLua.CheckObject(L, 1, typeof(UnityEngine.GameObject));
			System.Type arg1 = ToLua.CheckMonoType(L, 2);
			UnityEngine.Component o = Tool.GetAddComponent(arg0, arg1);
			ToLua.Push(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetComponent(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			UnityEngine.GameObject arg0 = (UnityEngine.GameObject)ToLua.CheckObject(L, 1, typeof(UnityEngine.GameObject));
			string arg1 = ToLua.CheckString(L, 2);
			System.Type arg2 = ToLua.CheckMonoType(L, 3);
			UnityEngine.Component o = Tool.GetComponent(arg0, arg1, arg2);
			ToLua.Push(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_NetAvailable(IntPtr L)
	{
		try
		{
			LuaDLL.lua_pushboolean(L, Tool.NetAvailable);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_IsWifi(IntPtr L)
	{
		try
		{
			LuaDLL.lua_pushboolean(L, Tool.IsWifi);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_AppWriteReadPath(IntPtr L)
	{
		try
		{
			LuaDLL.lua_pushstring(L, Tool.AppWriteReadPath);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_AppReadPath(IntPtr L)
	{
		try
		{
			LuaDLL.lua_pushstring(L, Tool.AppReadPath);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}
}


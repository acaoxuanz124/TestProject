using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//UnityUpgradable

public static class GameObjectExpand
{
    public static GameObject Parent(this GameObject gobj, Transform tran)
    {
        gobj.transform.SetParent(tran);
        return gobj;
    }
    public static GameObject Active(this GameObject gobj, bool active)
    {
        gobj.SetActive(active);
        return gobj;
    }
    public static T AddNotHaveComponent<T>(this GameObject gobj)
        where T : Component
    {
        T tValue = gobj.GetComponent<T>();
        if (!tValue)
            tValue = gobj.AddComponent<T>();
        
        return tValue;
    }
    public static T FindGetComponent<T>(this GameObject gobj, string strFind)
        where T : Component
    {
        T tValue = null;
        
        Transform findTran = gobj.transform.Find(strFind);
        if (typeof(T).Name == "Transform")
        {
            tValue = findTran as T;
        }
        else
        {
            tValue = gobj.GetComponent<T>();
        }
        return tValue;
    }
    public static GameObject SetName(this GameObject gobj, string name)
    {
        gobj.name = name;
        return gobj;
    }
    public static GameObject SetLayer(this GameObject gobj, int layer)
    {
        gobj.layer = layer;
        return gobj;
    }
    public static GameObject SetStatic(this GameObject gobj, bool isStatic)
    {
        gobj.isStatic = isStatic;
        return gobj;
    }


}

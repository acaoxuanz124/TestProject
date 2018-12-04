using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//UnityUpgradable

public static class GameObjectExpand
{
    public static GameObject Parent(this GameObject gobj, GameObject parentGobj)
    {
        gobj.transform.SetParent(parentGobj.transform);
        return gobj;
    }
    public static GameObject Active(this GameObject gobj, bool active)
    {
        gobj.SetActive(active);
        return gobj;
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

    public static GameObject SetPosition(this GameObject gobj, float x, float y, float z)
    {
        gobj.transform.position = new Vector3(x, y, z);
        return gobj;
    }
    public static GameObject SetPositionX(this GameObject gobj, float x)
    {
        Vector3 position = gobj.transform.position;
        position.x = x;
        gobj.transform.position = position;
        return gobj;
    }
    public static GameObject SetPositionY(this GameObject gobj, float y)
    {
        Vector3 position = gobj.transform.position;
        position.y = y;
        gobj.transform.position = position;
        return gobj;
    }
    public static GameObject SetPositionZ(this GameObject gobj, float z)
    {
        Vector3 position = gobj.transform.position;
        position.z = z;
        gobj.transform.position = position;
        return gobj;
    }
    public static GameObject SetLocalPosition(this GameObject gobj, float x, float y, float z)
    {
        gobj.transform.localPosition = new Vector3(x, y, z);
        return gobj;
    }
    public static GameObject SetLocalPositionX(this GameObject gobj, float x)
    {
        Vector3 localPosition = gobj.transform.localPosition;
        localPosition.x = x;
        gobj.transform.localPosition = localPosition;
        return gobj;
    }
    public static GameObject SetLocalPositionY(this GameObject gobj, float y)
    {
        Vector3 localPosition = gobj.transform.localPosition;
        localPosition.y = y;
        gobj.transform.localPosition = localPosition;
        return gobj;
    }
    public static GameObject SetLocalPositionZ(this GameObject gobj, float z)
    {
        Vector3 localPosition = gobj.transform.localPosition;
        localPosition.z = z;
        gobj.transform.localPosition = localPosition;
        return gobj;
    }
    public static GameObject SetScale(this GameObject gobj, float scale)
    {
        gobj.transform.localScale = new Vector3(scale, scale, scale);
        return gobj;
    }
    public static GameObject SetScale(this GameObject gobj, float x, float y, float z)
    {
        gobj.transform.localScale = new Vector3(x, y, z);
        return gobj;
    }
    public static GameObject FindGobj(this GameObject gobj,string findName)
    {
        Transform tran = gobj.transform.Find(findName);
        if (tran == null)
            return null;
        return tran.gameObject;
    }


}

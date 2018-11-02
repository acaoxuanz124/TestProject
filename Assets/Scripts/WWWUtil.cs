using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
public static class WWWUtil
{

    public static void Text(string url, Encoding encoding = null, UnityAction<string, string> doneAction = null)
    {
        Game.instance.StartCoroutine(Load(url, delegate (WWW www)
         {
             string text = string.Empty;
             if (encoding != null)
             {
                 text = encoding.GetString(www.bytes);
             }
             else
             {
                 text = www.text;
             }
             if (doneAction != null)
                 doneAction(www.error, text);
         }));
    }
    public static void Bytes(string url, UnityAction<string, byte[]> doneAction = null)
    {
        Game.instance.StartCoroutine(Load(url, delegate (WWW www)
        {
            if (doneAction != null)
                doneAction(www.error, www.bytes);
        }));
    }


    public static IEnumerator Load(string url, UnityAction<WWW> doneAction)
    {
        using (WWW www = new WWW(url))
        {
            yield return www;
            if (doneAction != null)
                doneAction(www);
        }
    }

}

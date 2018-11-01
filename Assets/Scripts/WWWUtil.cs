using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
public static class WWWUtil
{

    public static void Text(string url, Encoding encoding = null, UnityAction<string, string> doneAction = null)
    {
        Game.instance.StartCoroutine(_Load(url, delegate (WWW www)
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



    private static IEnumerator _Load(string url, UnityAction<WWW> doneAction)
    {
        using (WWW www = new WWW(url))
        {
            yield return www;
            if (doneAction != null)
                doneAction(www);
        }

    }

}

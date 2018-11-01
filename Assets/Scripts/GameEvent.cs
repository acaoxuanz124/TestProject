using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public static class GameEvent{
   public static readonly UnityEventEx Update = new UnityEventEx();
    public static readonly UnityEventEx Reset = new UnityEventEx();
    public static readonly UnityEventEx LateUpdate = new UnityEventEx();
    public static readonly UnityEventEx OnGUI = new UnityEventEx();

    public static readonly UnityEventEx SwitchScene = new UnityEventEx();
}

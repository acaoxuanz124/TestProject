using UnityEngine.Events;

public class UnityEventEx: UnityEvent
{
    public new void AddListener(UnityAction call)
    {
        if (call == null)
            return;
        base.AddListener(call);
    }
    public new void RemoveListener(UnityAction call)
    {
        if (call == null)
            return;
        base.RemoveListener(call);
    }
}
public class UnityEventEx<T0> : UnityEvent<T0>
{
    public new void AddListener(UnityAction<T0> call)
    {
        if (call == null)
            return;
        base.AddListener(call);
    }
    public new void RemoveListener(UnityAction<T0> call)
    {
        if (call == null)
            return;
        base.RemoveListener(call);
    }
}
public class UnityEventEx<T0,T1> : UnityEvent<T0, T1>
{
    public new void AddListener(UnityAction<T0, T1> call)
    {
        if (call == null)
            return;
        base.AddListener(call);
    }
    public new void RemoveListener(UnityAction<T0, T1> call)
    {
        if (call == null)
            return;
        base.RemoveListener(call);
    }
}
public class UnityEventEx<T0, T1,T2> : UnityEvent<T0, T1, T2>
{
    public new void AddListener(UnityAction<T0, T1, T2> call)
    {
        if (call == null)
            return;
        base.AddListener(call);
    }
    public new void RemoveListener(UnityAction<T0, T1, T2> call)
    {
        if (call == null)
            return;
        base.RemoveListener(call);
    }

}
public class UnityEventEx<T0, T1,T2,T3> : UnityEvent<T0, T1, T2, T3>
{
    public new void AddListener(UnityAction<T0, T1, T2, T3> call)
    {
        if (call == null)
            return;
        base.AddListener(call);
    }
    public new void RemoveListener(UnityAction<T0, T1, T2, T3> call)
    {
        if (call == null)
            return;
        base.RemoveListener(call);
    }

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State<T> where T : MonoBehaviour
{
    private T context;

    public State(T ctx)
    {
        context = ctx;
    }

    public virtual void EnterState() { }
    public virtual void UpdateState() { }
    public virtual void ExitState() { }
}

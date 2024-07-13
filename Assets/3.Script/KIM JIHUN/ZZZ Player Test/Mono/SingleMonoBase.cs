using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleMonoBase <T>: MonoBehaviour where T : SingleMonoBase<T>
{
    public static T INSTANCE;

    protected virtual void Awake()
    {
        if (INSTANCE != null)
            Debug.LogError(this + " �̹� ������");
        INSTANCE = (T)this;
    }

    protected virtual void OnDestroy()
    {
        Destroy();
    }

    public void Destroy()
    {
        INSTANCE = null;
    }
}

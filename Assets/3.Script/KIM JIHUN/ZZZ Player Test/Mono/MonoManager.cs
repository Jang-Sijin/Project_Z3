using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MonoManager : SingleMonoBase<MonoManager>
{
    public Action updateAction;
    public Action fixedUpdateAction;
    public Action lateUpdateAction;

    public void AddUpdateAction(Action task)
    {
        updateAction += task;
    }
    public void RemoveUpdateAction(Action task)
    {
        updateAction -= task;
    }
    public void AddFixedUpdateAction(Action task)
    {
        fixedUpdateAction += task;
    }

    public void RemoveFixedUpdateAction(Action task)
    {
        fixedUpdateAction -= task;
    }
    public void AddLateUpdateAction(Action task)
    {
        lateUpdateAction += task;
    }
    public void RemoveLateUpdateAction(Action task)
    {
        lateUpdateAction -= task;
    }

    private void Update()
    {
        updateAction?.Invoke();
    }

    private void FixedUpdate()
    {
        fixedUpdateAction?.Invoke();
    }

    private void LateUpdate()
    {
        lateUpdateAction?.Invoke();
    }
}

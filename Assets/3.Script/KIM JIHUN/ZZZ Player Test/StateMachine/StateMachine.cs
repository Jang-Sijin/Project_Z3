using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IStateMachineOwner { }

public class StateMachine
{
    private StateBase currentState;
    public bool hasState { get => currentState != null; }
    private IStateMachineOwner owner;
    private Dictionary<Type, StateBase> stateDic = new Dictionary<Type, StateBase>();

    public StateMachine(IStateMachineOwner owner)
    {
        Init(owner);
    }
    public void Init(IStateMachineOwner owner)
    {
        this.owner = owner;
    }

    public void EnterState<T>(bool reloadState = false) where T : StateBase, new()
    {
        if (hasState && currentState.GetType() == typeof(T) && !reloadState)
            return;

        if (hasState)
            ExitCurrentState();


        currentState = LoadState<T>();
        EnterCurrentState();
    }

    private StateBase LoadState<T>() where T : StateBase, new()
    {
        Type stateType = typeof(T);

        if (!stateDic.TryGetValue(stateType, out StateBase state))
        {
            state = new T();
            state.Init(owner);
            stateDic.Add(stateType, state);
        }

        return state;
    }

    private void EnterCurrentState()
    {
        currentState.Enter();
        MonoManager.INSTANCE.AddUpdateAction(currentState.Update);
        MonoManager.INSTANCE.AddFixedUpdateAction(currentState.FIxedUpdate);
        MonoManager.INSTANCE.AddLateUpdateAction(currentState.LateUpdate);
    }

    private void ExitCurrentState()
    {
        currentState.Exit();

        MonoManager.INSTANCE.RemoveUpdateAction(currentState.Update);
        MonoManager.INSTANCE.RemoveFixedUpdateAction(currentState.FIxedUpdate);
        MonoManager.INSTANCE.RemoveLateUpdateAction(currentState.LateUpdate);
    }

    public void Stop()
    {
        ExitCurrentState();
        foreach (var state in stateDic.Values)
        {
            state.UnInit();
        }
        stateDic.Clear();
    }
}

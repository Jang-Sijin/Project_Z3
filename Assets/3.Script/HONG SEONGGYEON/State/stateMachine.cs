using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public interface IstateMachineOwner { }
public class stateMachine
{
    private stateBase currentState;

    public bool hasState { get => currentState != null; }

    private IstateMachineOwner owner;

    private Dictionary<Type, stateBase> stateDic = new Dictionary<Type, stateBase>();

    public stateMachine(IstateMachineOwner owner)
    {
        Init(owner);
    }

    public void Init(IstateMachineOwner owner)
    {
        this.owner = owner;
    }
    //public void EnterState<T>() where T : stateBase, new()

    public void EnterState<T>(bool reloadState = false) where T : stateBase, new()
    {
        //Debug.Log("엔터스테이트");
        //if(currentState==null)
        //{
        //    Debug.Log("커렌트 없다");
        //}
        //
        //if (HasState && currentState.GetType() ==typeof(T))
        //
        //if (HasState)
        //{
        //    ExitCurrentState();
        //}
        //
        //currentState = LoadState<T>();
        //EnterCurrentState();
        if (hasState && currentState.GetType() == typeof(T) && !reloadState)
            return;

        if (hasState)
            ExitCurrentState();


        currentState = LoadState<T>();
        EnterCurrentState();
    }

    private stateBase LoadState<T>() where T : stateBase, new()
    {
        Type stateType = typeof(T);
        if (!stateDic.TryGetValue(stateType, out stateBase state))
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
        Debug.Log("엔터커랜트");
            MonoManager.INSTANCE.AddUpdateAction(currentState.Update);
            MonoManager.INSTANCE.AddFixedUpdateAction(currentState.FixedUpdate);
            MonoManager.INSTANCE.AddLateUpdateAction(currentState.LateUpdate);
        
    }

    private void ExitCurrentState()
    {
        currentState.Exit();
        Debug.Log("익싯커랜트");
        MonoManager.INSTANCE.RemoveUpdateAction(currentState.Update);
        MonoManager.INSTANCE.RemoveUpdateAction(currentState.FixedUpdate);
        MonoManager.INSTANCE.RemoveUpdateAction(currentState.LateUpdate);
    }

    public void Stop()
    {
        ExitCurrentState();
        foreach(var item in stateDic.Values)
        {
            item.UnInit();
        }
        stateDic.Clear();
    }
}

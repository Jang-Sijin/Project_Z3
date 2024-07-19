using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class stateBase

{
    public abstract void Init(IstateMachineOwner owner);
    public abstract void Enter();
    public abstract void Exit();
    public abstract void UnInit();

    public abstract void Update();
    public abstract void FixedUpdate();
    public abstract void LateUpdate();

}

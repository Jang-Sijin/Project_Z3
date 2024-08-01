using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearTweenManager : MonoBehaviour
{
    [SerializeField] private MovePanel levelUI;
    [SerializeField] private MovePanel deltatimeUI;
    [SerializeField] private MovePanel itemUI;
    [SerializeField] private MovePanel buttonUI;

    private void Start()
    {
        levelUI.GoToTargetPos();
        deltatimeUI.GoToTargetPos();
        itemUI.GoToTargetPos(); 
        buttonUI.GoToTargetPos();
    }
}

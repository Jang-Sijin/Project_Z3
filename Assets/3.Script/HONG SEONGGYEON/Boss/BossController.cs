using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossController : MonsterController
{
    private MonsterModel bossmodel;

    private void Awake()
    {
        ani = GetComponent<Animator>();
        statemachine = new stateMachine(this);
        mon_CO = GetComponent<MonCol_Control>();
        nmagent = GetComponent<NavMeshAgent>();
        bossmodel = GetComponent<MonsterModel>();
    }


}

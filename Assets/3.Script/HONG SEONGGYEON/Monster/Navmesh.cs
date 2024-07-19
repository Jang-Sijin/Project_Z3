using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Navmesh : MonoBehaviour
{
    public Transform Target;
    NavMeshAgent nmagent;
    [SerializeField]private Animator animator;

    private void Start()
    {
        nmagent = GetComponent<NavMeshAgent>();
        nmagent.updatePosition = false; // NavMeshAgent가 위치를 업데이트하지 않도록 설정
        nmagent.updateRotation = false; // NavMeshAgent가 회전을 업데이트하지 않도록 설정
    }

    private void Update()
    {
        if (Target != null)
        {
            nmagent.SetDestination(Target.position); // NavMeshAgent의 목적지 설정
        }
    }

    private void OnAnimatorMove()
    {
        if (nmagent != null)
        {
            Vector3 position = animator.rootPosition;
            transform.position = position;
        }
    }
}

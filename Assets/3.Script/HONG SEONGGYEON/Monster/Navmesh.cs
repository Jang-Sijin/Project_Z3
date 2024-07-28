using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Navmesh : MonoBehaviour
{
    public Transform Target;
    public NavMeshAgent nmagent;
    [SerializeField] private Animator animator;

    private void Start()
    {
        nmagent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        nmagent.updatePosition = false; // NavMeshAgent가 위치를 업데이트하지 않도록 설정
                                        //  nmagent.updateRotation = false; // NavMeshAgent가 회전을 업데이트하지 않도록 설정
    }

    private void Update()
    {
        if (Target != null)
        {

            nmagent.SetDestination(Target.position);
        }

    }

    private void OnAnimatorMove()
    {
        if (nmagent != null)
        {
            // 애니메이션 루트 모션을 사용하여 이동을 제어
            Vector3 newPosition = animator.rootPosition;
            transform.position = newPosition;

            // NavMeshAgent의 위치를 애니메이션 루트 모션의 위치로 수동으로 업데이트
            nmagent.nextPosition = transform.position;

        }
    }
}


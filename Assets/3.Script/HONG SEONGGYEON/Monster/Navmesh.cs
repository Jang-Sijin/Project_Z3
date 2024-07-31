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
        nmagent.updatePosition = false; // NavMeshAgent�� ��ġ�� ������Ʈ���� �ʵ��� ����
                                        //  nmagent.updateRotation = false; // NavMeshAgent�� ȸ���� ������Ʈ���� �ʵ��� ����
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
            // �ִϸ��̼� ��Ʈ ����� ����Ͽ� �̵��� ����
            Vector3 newPosition = animator.rootPosition;
            transform.position = newPosition;

            // NavMeshAgent�� ��ġ�� �ִϸ��̼� ��Ʈ ����� ��ġ�� �������� ������Ʈ
            nmagent.nextPosition = transform.position;

        }
    }
}


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
        nmagent.updatePosition = false; // NavMeshAgent�� ��ġ�� ������Ʈ���� �ʵ��� ����
        nmagent.updateRotation = false; // NavMeshAgent�� ȸ���� ������Ʈ���� �ʵ��� ����
    }

    private void Update()
    {
        if (Target != null)
        {
            nmagent.SetDestination(Target.position); // NavMeshAgent�� ������ ����
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Start()
    {
        CharacterController characterController = gameObject.GetComponent<CharacterController>();
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // �浹 ó�� ����
        Debug.Log("�浹 ����: " + hit.gameObject.name);
        // �ʿ��� �浹 ó�� ���� �߰�
    }
}

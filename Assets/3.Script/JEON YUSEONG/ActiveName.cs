using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveName : MonoBehaviour
{
    public GameObject player; // �÷��̾� ��ü
    public float interactionDistance = 1.0f; // ��ȣ�ۿ� �Ÿ�
    public GameObject nameTag; // NPC �Ӹ� ���� ǥ�õ� �̸� �±�

    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance <= interactionDistance)
        {
            // NPC �Ӹ� ���� �̸� ǥ��
            nameTag.SetActive(true);
        }
        else
        {
            // NPC �Ӹ� ���� �̸� ��Ȱ��ȭ
            nameTag.SetActive(false);
        }
    }
}

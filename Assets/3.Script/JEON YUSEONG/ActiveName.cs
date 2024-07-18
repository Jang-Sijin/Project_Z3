using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveName : MonoBehaviour
{
    public GameObject player; // 플레이어 객체
    public float interactionDistance = 1.0f; // 상호작용 거리
    public GameObject nameTag; // NPC 머리 위에 표시될 이름 태그

    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance <= interactionDistance)
        {
            // NPC 머리 위에 이름 표시
            nameTag.SetActive(true);
        }
        else
        {
            // NPC 머리 위에 이름 비활성화
            nameTag.SetActive(false);
        }
    }
}

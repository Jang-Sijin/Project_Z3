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
        // 충돌 처리 로직
        Debug.Log("충돌 감지: " + hit.gameObject.name);
        // 필요한 충돌 처리 로직 추가
    }
}

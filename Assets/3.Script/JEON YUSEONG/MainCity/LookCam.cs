using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LookCam : MonoBehaviour
{
    public string npcName;
    public GameObject nameTagUI;
    public GameObject arrowUI;
    public float activationDistance = 5f;
    public float arrowActivationDistance = 2f;
    public Camera playerCamera;

    private Transform player;

    void Start()
    {
        // 플레이어 카메라가 설정되지 않은 경우 메인 카메라를 사용
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }

        player = playerCamera.transform;
        nameTagUI.SetActive(false);
        arrowUI.SetActive(false);
    }

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= activationDistance)
        {
            nameTagUI.SetActive(true);
            RotateUI(nameTagUI);

            if (distance <= arrowActivationDistance && IsPlayerLookingAtNPC())
            {
                arrowUI.SetActive(true);
            }
            else
            {
                arrowUI.SetActive(false);
            }
        }
        else
        {
            nameTagUI.SetActive(false);
            arrowUI.SetActive(false);
        }
    }

    void RotateUI(GameObject uiElement)
    {
        Vector3 directionToPlayer = player.position - uiElement.transform.position;
        directionToPlayer.y = 0f;
        uiElement.transform.rotation = Quaternion.LookRotation(directionToPlayer);
        uiElement.transform.Rotate(0, 180, 0); // 이 줄을 추가하여 UI가 정방향으로 보이도록
    }

    bool IsPlayerLookingAtNPC()
    {
        Vector3 directionToNPC = (transform.position - player.position).normalized;
        float dotProduct = Vector3.Dot(player.forward, directionToNPC);
        return dotProduct > 0.95f;
    }
}

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
        player = Camera.main.transform;
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
        nameTagUI.transform.Rotate(0, 180, 0);
    }

    bool IsPlayerLookingAtNPC()
    {
        Vector3 directionToNPC = (transform.position - player.position).normalized;
        float dotProduct = Vector3.Dot(player.forward, directionToNPC);
        return dotProduct > 0.95f;
    }
}

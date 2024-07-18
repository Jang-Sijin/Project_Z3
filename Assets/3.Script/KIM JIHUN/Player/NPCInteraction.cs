using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public string npcName;
    public GameObject nameTagUI;
    public GameObject arrowUI;
    public GameObject arrowUI2;
    public float activationDistance = 5f;
    public float arrowActivationDistance = 2f;
    public Camera playerCamera;

    private Transform player;

    void Start()
    {
        player = Camera.main.transform;
        nameTagUI.SetActive(false);
        arrowUI.SetActive(false);
        arrowUI2.SetActive(false);
    }

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= activationDistance)
        {
            nameTagUI.SetActive(true);
            nameTagUI.transform.LookAt(player);
            nameTagUI.transform.Rotate(0, 180, 0);

            if (distance <= arrowActivationDistance && IsPlayerLookingAtNPC())
            {
                arrowUI.SetActive(true);
                arrowUI2.SetActive(true);
            }
            else
            {
                arrowUI.SetActive(false);
                arrowUI2.SetActive(false);
            }
        }
        else
        {
            nameTagUI.SetActive(false);
            arrowUI.SetActive(false);
            arrowUI2.SetActive(false);
        }

        float scale = Vector3.Distance(player.position, transform.position) / activationDistance;
        nameTagUI.transform.localScale = new Vector3(scale, scale, scale);
        arrowUI.transform.localScale = new Vector3(scale, scale, scale);
        arrowUI2.transform.localScale = new Vector3(scale, scale, scale);
    }

    bool IsPlayerLookingAtNPC()
    {
        Vector3 directionToNPC = (transform.position - player.position).normalized;
        float dotProduct = Vector3.Dot(player.forward, directionToNPC);
        return dotProduct > 0.9f;
    }
}

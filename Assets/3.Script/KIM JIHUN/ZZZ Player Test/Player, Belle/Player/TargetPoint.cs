using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPoint : MonoBehaviour
{
    private float height;
    private void Awake()
    {
        height = transform.position.y;
    }
    private void LateUpdate()
    {
        Vector3 playerPos = PlayerController.INSTANCE.playerModel.transform.position;
        transform.position = new Vector3(playerPos.x, playerPos.y + height, playerPos.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
            PlayerController.INSTANCE.AddEnemy(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
            PlayerController.INSTANCE.RemoveEnemy(other.gameObject);
    }
}

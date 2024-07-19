using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    [HideInInspector] public BoxCollider weaponBoxCol;
    private bool isShakeTrigger;
    private bool isEnemyDetect;
    private void Awake()
    {
        weaponBoxCol = transform.GetComponent<BoxCollider>();
        isShakeTrigger = false;
        isEnemyDetect = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            isEnemyDetect = true;
            if (isShakeTrigger)
                PlayerController.INSTANCE.ShakeCamera(1.5f, 0.1f);
        }
    }

    public void SetShakeTrigger(bool value)
    {
        isShakeTrigger = value;
    }

}

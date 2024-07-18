using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BelleTargetPoint : MonoBehaviour
{
    private float height;

    private void Awake()
    {
        height = transform.position.y;
    }

    private void LateUpdate()
    {
        Vector3 bellePos = BelleController.INSTANCE.belleModel.transform.position;
        transform.position = new Vector3(bellePos.x, bellePos.y + height, bellePos.z);
    }
}

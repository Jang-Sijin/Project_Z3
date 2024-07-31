using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapColider : MonoBehaviour
{
    private Collider mapCol;
    private float currentTime=0;

    private void Start()
    {
        mapCol = GetComponent<Collider>();
        mapCol.enabled = true;
    }

    private void Update()
    {
        currentTime += Time.deltaTime;

        if(currentTime>30.0f)
        {
            mapCol.enabled = false;
        }
    }
}

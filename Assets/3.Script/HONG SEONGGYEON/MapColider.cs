using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapColider : MonoBehaviour
{
    private Collider mapCol;


    private void Start()
    {
        mapCol = GetComponent<Collider>();
        mapCol.enabled = true;
    }

    private void Update()
    {
        
    }
}

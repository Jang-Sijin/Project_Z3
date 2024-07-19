using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonCol_Control : MonoBehaviour
{
    //   private Collider[] Weaponcolliders;
    private Collider[] Weaponcolliders;

    private void Start()
    {
               Weaponcolliders = GetComponentsInChildren<Collider>();
        
               foreach (Collider col in Weaponcolliders)
               {
                   if (col.CompareTag("EnemyWeapon")) col.enabled = false;
                   else col.enabled = true;
        
               }
    }

    public void EnableWeaponCollider()
    {
          foreach (Collider col in Weaponcolliders)
          {
              col.enabled = true;
              Debug.Log("활성");
          }
       
    }

    public void DisableWeaponCollider()
    {
          foreach (Collider col in Weaponcolliders)
          {
              col.enabled = false;
              Debug.Log("비활성");
          }

    }


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("온트리거엔터");
        foreach (Collider col in Weaponcolliders)
        {
            if (col.CompareTag("EnemyWeapon") && collision.gameObject.CompareTag("Player"))
            {
                Debug.Log("때림");
            }
        }

    }

}

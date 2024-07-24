using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build_InteractableObject : MonoBehaviour
{

    private void OnTriggerStay(Collider other)
    {
        if(BelleController.INSTANCE != null)
        {
            if (BelleController.INSTANCE.playerInputSystem.Player.Interact.triggered)
            {
                transform.GetComponent<Build_Interact>().Interact();
                //BelleController.INSTANCE.CanInput = false;
            }
        }
        else
        {
            if(PlayerController.INSTANCE.playerInputSystem.Player.Interact.triggered)
            {
                transform.GetComponent <Build_Interact>().Interact();
                //PlayerController.INSTANCE.CanInput = false;
            }
        }
    }
}

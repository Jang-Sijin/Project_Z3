using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTest : MonoBehaviour
{    
    void Start()
    {
        SoundManager.Instance.PlayBgm("bgm_SwordLand");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            SoundManager.Instance.PlayEffect("uni1464");
        }
    }
}

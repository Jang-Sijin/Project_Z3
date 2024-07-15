using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Control : MonoBehaviour
{

   
    [SerializeField] public float mon_HP = 100f;
    [SerializeField] public float mon_GP = 100f;
    public Player_Control player;
    private Animator mon_ani;
    public bool isGroggy = false;

    private void Start()
    {
        mon_ani = GetComponent<Animator>();
        player = FindObjectOfType<Player_Control>();
    }

    private void Update()
    {
        if (mon_GP <= 0f && !isGroggy)  // 그로기 상태 진입
        {
            isGroggy = true;
            player.Attack = 10;
            player.GroggyPoint = 0;
            StartCoroutine(EnterGroggy_co());
        }
        if (mon_HP <= 0)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator EnterGroggy_co()
    {
        mon_ani.SetTrigger("isGroggy");
        yield return new WaitForSeconds(0.5f);

        mon_ani.SetBool("isGroggyLoop", true);
        yield return new WaitForSeconds(10f);
        mon_ani.SetBool("isGroggyLoop", false);
        mon_GP = 100f;

        isGroggy = false;
    }

   // public void OnCollisionEnter(Collision collision)
   // {
   //     if (collision.gameObject.CompareTag("Player"))
   //     {
   //         mon_HP -= 1;
   //         Debug.Log($"{mon_HP}남음");
   //     }
   // }
}

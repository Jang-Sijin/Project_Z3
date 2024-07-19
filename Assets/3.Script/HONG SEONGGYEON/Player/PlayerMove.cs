using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5.0f; // 이동 속도

    void Update()
    {
        // 입력 받기
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // 이동 방향 설정
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // 이동
        transform.Translate(movement * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyWeapon"))
        {
            Debug.Log("맞음");
        }
    }
}



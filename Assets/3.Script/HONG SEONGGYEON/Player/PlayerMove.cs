using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5.0f; // �̵� �ӵ�

    void Update()
    {
        // �Է� �ޱ�
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // �̵� ���� ����
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // �̵�
        transform.Translate(movement * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyWeapon"))
        {
            Debug.Log("����");
        }
    }
}



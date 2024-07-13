using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject playerObj;
    [HideInInspector]public float cameraArmHeight;

    private void Start()
    {
        cameraArmHeight = transform.position.y;
    }
    private void Update()
    {
        MoveCamera();
        LookAround();
    }
    public void MoveCamera()
    {
        this.transform.position = new Vector3(playerObj.transform.position.x, cameraArmHeight, playerObj.transform.position.z);
    }
    public void LookAround()
    {
        // ���콺�� �����¿� ���� ��Ʈ��
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector3 camAngle = transform.rotation.eulerAngles;

        float cameraX = camAngle.x - mouseDelta.y;
        if (cameraX < 180f) //ī�޶� ȸ������ 180�� �̸��� ��� -> ī�޶� ���� ȸ���� ��
        {
            cameraX = Mathf.Clamp(cameraX, -1f, 50f);
        }
        else // ī�޶� �Ʒ��� ȸ���� ���
        {
            cameraX = Mathf.Clamp(cameraX, 335f, 361f);
        }

        transform.rotation = Quaternion.Euler(cameraX, camAngle.y + mouseDelta.x, camAngle.z);
    }

}

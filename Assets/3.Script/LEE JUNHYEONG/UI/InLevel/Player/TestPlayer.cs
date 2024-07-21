using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    private Rigidbody Rigidbody;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    private float z; // ╬у╣з
    private float x; // аб©Л

    private void FixedUpdate()
    {
        Inputs();
        Move();
    }

    private void Inputs()
    {
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");
    }

    private void Move()
    {
        Rigidbody.transform.position += new Vector3(x, 0, z) * Time.deltaTime *10f;
        transform.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}

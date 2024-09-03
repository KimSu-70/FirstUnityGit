using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController4 : MonoBehaviour
{
    [SerializeField] float moveSpeed;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(x, 0, z).normalized;

        if (move != Vector3.zero)
        {
            transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);

            Vector3 Position = transform.position + move;
            transform.LookAt(Position);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController2 : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float rotateSpeed;

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        // 전진 후진
        transform.Translate(Vector3.forward * z * moveSpeed * Time.deltaTime, Space.Self);

        // 회전
        transform.Rotate(Vector3.up, x * rotateSpeed * Time.deltaTime, Space.World);
    }
}

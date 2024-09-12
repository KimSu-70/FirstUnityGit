using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] Transform camTransform;
    [SerializeField] float moveSpeed;

    private void Start()
    {
        camTransform = Camera.main.transform;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        // 카메라가 바라보는 방향을 기준으로 움직이기 위해
        // 카메라의 방향에서 y는 0으로 하는 방향을 구현 (땅바닥을 뚫도록 움직이는 경우 방지)
        Vector3 forwardDir = new Vector3(camTransform.forward.x, 0, camTransform.forward.z).normalized;
        Vector3 rightDir = new Vector3(camTransform.right.x, 0, camTransform.right.z).normalized;

        Vector3 dir = forwardDir * z + rightDir * x;
        if (dir == Vector3.zero)
            return;

        transform.Translate(dir.normalized * moveSpeed * Time.deltaTime, Space.World);
        transform.rotation = Quaternion.LookRotation(dir);
    }
}

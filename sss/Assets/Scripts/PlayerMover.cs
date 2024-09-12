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

        // ī�޶� �ٶ󺸴� ������ �������� �����̱� ����
        // ī�޶��� ���⿡�� y�� 0���� �ϴ� ������ ���� (���ٴ��� �յ��� �����̴� ��� ����)
        Vector3 forwardDir = new Vector3(camTransform.forward.x, 0, camTransform.forward.z).normalized;
        Vector3 rightDir = new Vector3(camTransform.right.x, 0, camTransform.right.z).normalized;

        Vector3 dir = forwardDir * z + rightDir * x;
        if (dir == Vector3.zero)
            return;

        transform.Translate(dir.normalized * moveSpeed * Time.deltaTime, Space.World);
        transform.rotation = Quaternion.LookRotation(dir);
    }
}

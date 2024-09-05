using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //[SerializeField] Transform target;
    //[SerializeField] Vector3 offset;
    //[SerializeField] float rotateSpeed;

    //private void LateUpdate()  // �������� ���Ŀ� ����
    //{

    //}

    // �÷��̾� ĳ������ Transform�� �����ϴ� �����Դϴ�.
    public Transform player;

    // ���콺 ������ ī�޶��� ȸ�� ������ �����ϴ� �����Դϴ�.
    public float mouseSensitivity = 100f;
    public float clampAngle = 80f;

    // ī�޶��� ���� ȸ���� �����ϴ� �����Դϴ�.
    private float xRotation = 0f;

    private void Update()
    {
        // ���콺 ������ ��ư�� ���� ���� ���� ī�޶� ȸ����ŵ�ϴ�.
        if (Input.GetMouseButton(1))
        {
            // ���콺�� X��� Y�� �̵��� ������ ���� �����մϴ�.
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

            // ���� ȸ���� ������Ʈ�մϴ�. ȸ�� ������ �����Ͽ� ī�޶� �����ϰ� �������� �ʵ��� �մϴ�.
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -clampAngle, clampAngle);

            // ī�޶��� ���� ȸ��(����)�� �����մϴ�.
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            // �÷��̾� ĳ������ ���� ȸ���� �����մϴ�. ���콺 X�� �̵��� ���� ĳ���Ͱ� ȸ���մϴ�.
            player.Rotate(Vector3.up * mouseX);
        }

        // ī�޶� �׻� �÷��̾��� ��ġ�� ���󰡰� �����մϴ�.
        transform.position = player.position;
    }
}

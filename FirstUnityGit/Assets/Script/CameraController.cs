using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //[SerializeField] Transform target;
    //[SerializeField] Vector3 offset;
    //[SerializeField] float rotateSpeed;

    //private void LateUpdate()  // 업데이후 이후에 실행
    //{

    //}

    // 플레이어 캐릭터의 Transform을 참조하는 변수입니다.
    public Transform player;

    // 마우스 감도와 카메라의 회전 제한을 설정하는 변수입니다.
    public float mouseSensitivity = 100f;
    public float clampAngle = 80f;

    // 카메라의 수직 회전을 저장하는 변수입니다.
    private float xRotation = 0f;

    private void Update()
    {
        // 마우스 오른쪽 버튼이 눌려 있을 때만 카메라를 회전시킵니다.
        if (Input.GetMouseButton(1))
        {
            // 마우스의 X축과 Y축 이동을 감도에 따라 조정합니다.
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

            // 수직 회전을 업데이트합니다. 회전 각도를 제한하여 카메라가 과도하게 기울어지지 않도록 합니다.
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -clampAngle, clampAngle);

            // 카메라의 수직 회전(상하)을 적용합니다.
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            // 플레이어 캐릭터의 수평 회전을 적용합니다. 마우스 X축 이동에 따라 캐릭터가 회전합니다.
            player.Rotate(Vector3.up * mouseX);
        }

        // 카메라가 항상 플레이어의 위치를 따라가게 설정합니다.
        transform.position = player.position;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float distance;
    [SerializeField] float lerpRate;

    [SerializeField] float mouseSensitivity;
    [SerializeField] float yAngle;
    [SerializeField] float xAngle;

    private float curDistance;

    private void Start()
    {
        xAngle = transform.eulerAngles.x;
        yAngle = transform.eulerAngles.y;
    }

    private void LateUpdate()
    {
        Rotate();
        Move();
    }

    private void Rotate()
    {
        if (Input.GetMouseButton(1) == false)
            return;

        xAngle -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        yAngle += Input.GetAxis("Mouse X") * mouseSensitivity;
    }

    private void Move()
    {
        // 기본 실습
        // transform.rotation = Quaternion.Euler(xAngle, yAngle, 0);
        // transform.position = target.position - transform.forward * distance;

        // 심화실습
        transform.rotation = Quaternion.Euler(xAngle, yAngle, 0);

        float targetDistance;
        if (Physics.Raycast(target.position, -transform.forward, out RaycastHit hitInfo, distance))
        {
            Debug.DrawRay(target.position, -transform.forward * hitInfo.distance, Color.red);
            targetDistance = hitInfo.distance;
        }
        else
        {
            Debug.DrawRay(target.position, -transform.forward * distance, Color.red);
            targetDistance = distance;
        }

        curDistance = Mathf.Lerp(curDistance, targetDistance, lerpRate * Time.deltaTime);
        transform.position = target.position - transform.forward * curDistance;
    }
}

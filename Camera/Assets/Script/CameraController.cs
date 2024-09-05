using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float xAngle;
    [SerializeField] float yAngle;

    [SerializeField] float mouseSensitivity;
    [SerializeField] float distance;

    [SerializeField] Transform target;

    private void LateUpdate()
    {
        Rotate();
        Move();
    }

    private void Rotate()
    {
        if (Input.GetMouseButton(1) == false)
        { return; }

        xAngle -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        yAngle += Input.GetAxis("Mouse X") * mouseSensitivity;
    }

    private void Move()
    {
        transform.rotation = Quaternion.Euler(xAngle, yAngle, 0);
        transform.position = target.position - transform.forward * distance;
    }
}

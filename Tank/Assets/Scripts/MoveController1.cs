using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController1 : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float rotateSpeed;
    [SerializeField] float min;
    [SerializeField] float max;

    private void Update()
    {
        float x = Input.GetAxis("Maingun1");
        float z = Input.GetAxis("Maingun2");


        Vector3 currentRotation = transform.eulerAngles;


        float clampedX = Mathf.Clamp(NormalizeAngle(currentRotation.x) + z * moveSpeed * Time.deltaTime, min, max);


        float newRotationY = currentRotation.y + x * rotateSpeed * Time.deltaTime;


        transform.eulerAngles = new Vector3(clampedX, newRotationY, currentRotation.z);
    }

    private float NormalizeAngle(float angle)
    {
        if (angle > 180f)
            angle -= 360f;
        return angle;
    }

}

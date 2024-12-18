using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMainCamera : MonoBehaviour
{
    public GameObject Target;

    public float offsetX;
    public float offsetY;
    public float offsetZ;

    public float CameraSpeed = 10.0f;
    Vector3 TargetPos;

    void LateUpdate()
    {

        TargetPos = new Vector3(
            Target.transform.position.x + offsetX,
            Target.transform.position.y + offsetY,
            Target.transform.position.z + offsetZ
            );


        transform.position = Vector3.Lerp(transform.position, TargetPos, Time.deltaTime * CameraSpeed);
    }
}

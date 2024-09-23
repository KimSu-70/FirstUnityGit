using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTester : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] float moveSpeed;

    private float ySpeed;

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(x, 0, z).normalized;

        controller.Move(dir * moveSpeed * Time.deltaTime);

        if (controller.isGrounded == false)
        {
            ySpeed -= Physics.gravity.y * Time.deltaTime;
            controller.Move(Vector3.down * ySpeed * Time.deltaTime);
        }
        else
        {
            ySpeed = 0f;
        }
    }
}

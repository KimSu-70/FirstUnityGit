using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    [SerializeField] Rigidbody rigid;
    [SerializeField] PlayerInput input;

    [SerializeField] float movePower;
    [SerializeField] float jumpPower;

    private void Update()
    {
        Vector2 move = input.actions["Move"].ReadValue<Vector2>();
        Vector3 dir = new Vector3(move.x, 0, move.y);
        rigid.velocity = dir * movePower + Vector3.up * rigid.velocity.y;
        // rigid.AddForce(dir * movePower, ForceMode.Force);

        Debug.Log(move);

        //IsPressed(); = GetKey
        //WasPressedThisFrame() = GetKeyDown
        //WasReleasedThisFrame(); = GetKeyUp

        bool jump = input.actions["Jump"].WasPressedThisFrame();

        if (jump)
        {
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
    }
}

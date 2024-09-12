using System.Collections;
using UnityEngine;

public class PlayerContrllen : MonoBehaviour
{
    [SerializeField] PlayerModel model;
    [SerializeField] Animator animator;

    private void Update()
    {
        Move();
        Fire();
    }
    private void Move()
    {

        float y = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 moveDir = transform.forward * z;
        if (moveDir.magnitude > 1) moveDir.Normalize();

        transform.Translate(moveDir * model.MoveSpeed * Time.deltaTime, Space.World);
        animator.SetFloat("Speed", z * model.MoveSpeed);
        transform.Rotate(Vector3.up * y * model.RotateSpeed * Time.deltaTime, Space.World);
    }
    private void Fire()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetBool("Fire", true);
        }
        if (Input.GetMouseButtonUp(0))
        {
            animator.SetBool("Fire", false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AnimatorTester : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] float speed;

    private void Update()
    {
        float z = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.forward * speed * z * Time.deltaTime);
        animator.SetFloat("Speed", speed * z);
    }
}

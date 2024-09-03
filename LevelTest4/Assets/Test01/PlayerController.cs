using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed;

    public event Action OnDied;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 move = new Vector3(x, 0, z).normalized;

        if (move != Vector3.zero)
        {
            transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);

            Vector3 Position = transform.position + move;
            transform.LookAt(Position);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Monster")
        {
            OnDied?.Invoke();
            Destroy(gameObject);
        }
    }
}

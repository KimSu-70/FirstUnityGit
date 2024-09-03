using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet6 : MonoBehaviour
{
    [SerializeField] float speed;

    private void Start()
    {
        Destroy(gameObject, 2);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Monster")
        {
            Monster monster = collision.gameObject.GetComponent<Monster>();
            Destroy(monster.gameObject);
            Destroy(gameObject);
        }
    }
}

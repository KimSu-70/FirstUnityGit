using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet5 : MonoBehaviour
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
}

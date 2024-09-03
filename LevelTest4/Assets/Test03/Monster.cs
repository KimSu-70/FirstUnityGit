using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Monster : MonoBehaviour
{
    [SerializeField] Transform pos;
    [SerializeField] Transform target;
    [SerializeField] LayerMask layerMask;

    [SerializeField] float speed;
    [SerializeField] float maxDistance;

    private void Start()
    {
        GameObject PlayerObj = GameObject.FindGameObjectWithTag("Player");

        target = PlayerObj.GetComponent<Transform>();

        target = PlayerObj.transform;
    }

    private void Update()
    {
        transform.Rotate(Vector3.up * 60 * Time.deltaTime);

        if (Physics.Raycast(pos.position, pos.forward, out RaycastHit hit, maxDistance, layerMask))
        {

            if (hit.collider.gameObject.tag == "Player")
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                transform.LookAt(target.position);
            }
        }
    }
}

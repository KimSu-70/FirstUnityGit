using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    [SerializeField] Transform coin;
    [SerializeField] float rotateSpeed;

    private void Update()
    {
        coin.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
    }
}

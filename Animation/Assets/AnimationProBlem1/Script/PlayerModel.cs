using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] float moveSpeed;
    public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }

    [SerializeField] float rotateSpeed;
    public float RotateSpeed { get { return rotateSpeed; } set { rotateSpeed = value; } }
}

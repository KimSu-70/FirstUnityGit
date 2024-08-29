using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // �Κ����� â���� �����ϰų� ���� �������־ ���� ���
    // ������� : ����
    [SerializeField] Rigidbody rigid;
    [SerializeField] float moveSpeed;

    [SerializeField] int hp;    // �÷��̾� hp
    [SerializeField] private Color[] hpColors;
    private Renderer playerR;

    public event Action OnDied;


    private void Start()
    {
        playerR = GetComponent<Renderer>();

        if (hpColors.Length > 0)
        {
            playerR.material.color = hpColors[hp - 1];
        }

    }

    //����Ƽ �޽��� �Լ����� ������ ���ҿ� �°� ä�������� ��ɿ� ���ؼ� ����
    //����Լ� : ����
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        // �Է� ���� �޾� ����
        // �Է¸Ŵ��� : Edit -> ProjectSetting ���� ������ �̸��� �Է� ����� ���
        // GetAxis() : �� �Է� -1 ~ +1 float �� -> ���̽�ƽó�� �����Էµ� ����
        // GetAxisRaw() : �� �Է� -1, 0, +1 float �� �Ҽ��� ���� �Է� ���� �Ǵ� -> Ű����ó�� ������ �ִ� ��쿡 ����
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");


        // �ܼ� : ������ ������ �ؽ�Ʈ ���·� �����ڿ��� �˷��ִ� â
        //Debug.Log($"{x}, {z}");

        // ����ȭ : ũ�Ⱑ 1�� �ƴ� ���͸� ũ�⸦ 1�� �����
        Vector3 moveDir = new Vector3(x, 0, z);

        if(moveDir.magnitude > 1)   // magnitude : ������ ũ��
                                    // sqrmagnitude : 
        {
            moveDir.Normalize();
        }
        
        // ������ٵ� : ������������� ������Ʈ
        // AddForce, AddTorque, velocity, angularVelocity
        rigid.velocity = moveDir * moveSpeed;
    }

    public void TakeHit()
    {
        if (hp > 0)
        {
            hp--;

            // hp ���� ���� ���� ����
            if (hp > 0 && hpColors.Length > 0)
            {
                int colorIndex = Mathf.Clamp(hp - 1, 0, hpColors.Length - 1);
                playerR.material.color = hpColors[colorIndex];
            }

            if (hp <= 0)
            {
                OnDied?.Invoke();
                Destroy(gameObject);
            }
        }
    }

}

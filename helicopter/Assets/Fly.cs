using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Fly : MonoBehaviour
{
    [SerializeField] private float InitialSpeed;        //�ʱ� �ӵ�
    [SerializeField] private float CurrentSpeed;        //���� �ӵ�
    [SerializeField] private float Acceleration;        //���ӵ�
    [SerializeField] private float Deceleration;        //����
    [SerializeField] private float InitialUp;           //�ʱ� ���
    [SerializeField] private float Speed;               //�߰� ��� �ӵ�
    [SerializeField] private float Max = 50f;           //�ִ� ����
    [SerializeField] private float moveSpeed;           //�̵� �ӵ�

    public Transform eggbeater;                         //�����緯
    public Rigidbody HelicopterBody;                    //�︮���͹ٵ�

    void Update()
    {
        Eggbeater();
        MaxUp();
    }

    private void Eggbeater()
    {
        Application.targetFrameRate = 60;
        if (Input.GetKey(KeyCode.Space))
        {
            CurrentSpeed += InitialSpeed * Time.deltaTime;
            
            CurrentSpeed += Acceleration * Time.deltaTime;

            eggbeater.Rotate(Vector3.up * CurrentSpeed * Time.deltaTime, Space.World);
        }
        else if (CurrentSpeed > 0)  
        {
            CurrentSpeed -= Deceleration * Time.deltaTime;

            if (CurrentSpeed < 0)
            {
                CurrentSpeed = 0;
            }
            eggbeater.Rotate(Vector3.up * CurrentSpeed * Time.deltaTime, Space.World);
        }

        if (CurrentSpeed > 2000)         //�ִ� �ӵ��� 600�̻��϶� ��� ����
        {
            InitialUp += Speed * Time.deltaTime;
            HelicopterBody.AddForce(Vector3.up * InitialUp, ForceMode.Impulse);

            CurrentSpeed = 2000;         //�ִ� �ӵ� ����
        }
    }

    private void MaxUp()
    {
        Vector3 maxup = transform.position;

        if (maxup.y > 1)            // ���� ��ġ�� y���� 1���� ũ�� �̵� ����
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = new Vector3 (x, 0, z);

            transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);
        }
        
        // ���� ��ġ�� y ���� �ִ� ������ ũ��
        if (maxup.y > Max)
        {
            // y ���� �ִ� ���� ����
            maxup.y = Max;
            transform.position = maxup;

            HelicopterBody.velocity = new Vector3(HelicopterBody.velocity.x, 0, HelicopterBody.velocity.z);
            InitialUp = 80;
        }
    }
}

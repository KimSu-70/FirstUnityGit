using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Fly : MonoBehaviour
{
    [SerializeField] private float InitialSpeed;        //초기 속도
    [SerializeField] private float CurrentSpeed;        //현재 속도
    [SerializeField] private float Acceleration;        //가속도
    [SerializeField] private float Deceleration;        //감속
    [SerializeField] private float InitialUp;           //초기 상승
    [SerializeField] private float Speed;               //추가 상승 속도
    [SerializeField] private float Max = 50f;           //최대 높이
    [SerializeField] private float moveSpeed;           //이동 속도

    public Transform eggbeater;                         //프로펠러
    public Rigidbody HelicopterBody;                    //헬리콥터바디

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

        if (CurrentSpeed > 2000)         //최대 속도가 600이상일때 상승 시작
        {
            InitialUp += Speed * Time.deltaTime;
            HelicopterBody.AddForce(Vector3.up * InitialUp, ForceMode.Impulse);

            CurrentSpeed = 2000;         //최대 속도 제한
        }
    }

    private void MaxUp()
    {
        Vector3 maxup = transform.position;

        if (maxup.y > 1)            // 현재 위치의 y값이 1보다 크면 이동 가능
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = new Vector3 (x, 0, z);

            transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);
        }
        
        // 현재 위치의 y 값이 최대 고도보다 크면
        if (maxup.y > Max)
        {
            // y 값을 최대 고도로 제한
            maxup.y = Max;
            transform.position = maxup;

            HelicopterBody.velocity = new Vector3(HelicopterBody.velocity.x, 0, HelicopterBody.velocity.z);
            InitialUp = 80;
        }
    }
}

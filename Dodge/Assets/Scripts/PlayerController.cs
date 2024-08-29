using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 인슾펙터 창에서 참조하거나 값을 지정해주어서 쓰는 경우
    // 멤버변수 : 정보
    [SerializeField] Rigidbody rigid;
    [SerializeField] float moveSpeed;

    [SerializeField] int hp;    // 플레이어 hp
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

    //유니티 메시지 함수들을 각각의 역할에 맞게 채워넣으며 기능에 대해서 구현
    //멤버함수 : 동작
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        // 입력 값을 받아 저장
        // 입력매니저 : Edit -> ProjectSetting 에서 설정한 이름의 입력 방식을 사용
        // GetAxis() : 축 입력 -1 ~ +1 float 값 -> 조이스틱처럼 조금입력도 가능
        // GetAxisRaw() : 축 입력 -1, 0, +1 float 값 소수점 없이 입력 여부 판단 -> 키보드처럼 누르고 있는 경우에 적합
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");


        // 콘솔 : 게임의 정보를 텍스트 형태로 제작자에게 알려주는 창
        //Debug.Log($"{x}, {z}");

        // 정규화 : 크기가 1이 아닌 백터를 크기를 1로 만들기
        Vector3 moveDir = new Vector3(x, 0, z);

        if(moveDir.magnitude > 1)   // magnitude : 백터의 크기
                                    // sqrmagnitude : 
        {
            moveDir.Normalize();
        }
        
        // 리지드바디 : 물리엔진담당의 컴포넌트
        // AddForce, AddTorque, velocity, angularVelocity
        rigid.velocity = moveDir * moveSpeed;
    }

    public void TakeHit()
    {
        if (hp > 0)
        {
            hp--;

            // hp 값에 따라 색상 변경
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

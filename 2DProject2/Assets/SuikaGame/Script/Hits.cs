using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hits : MonoBehaviour
{
    [SerializeField] private int Level; // 행성의 현재 단계
    [SerializeField] private float sizeIncrement; // 단계별 크기 증가량

    private Transform planetTransform; // 행성의 Transform 컴포넌트
    private SpriteRenderer spriteRenderer; // 행성의 SpriteRenderer 컴포넌트

    private void Start()
    {
        // Transform과 SpriteRenderer 컴포넌트를 캐싱
        planetTransform = transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdatePlanetSize(); // 크기 설정
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌한 물체에서 Hits 컴포넌트를 가져옴
        Hits Updates = collision.gameObject.GetComponent<Hits>();

        // 충돌한 행성이 같은 레벨일 때만 합성 처리
        if (Updates && Updates.Level == Level)
        {
            MergePlanets(Updates); // 행성 합성
        }
    }

    private void MergePlanets(Hits Updates)
    {
        Destroy(Updates.gameObject); // 충돌한 행성 삭제
        Level++; // 레벨 증가
        UpdatePlanetSize(); // 크기 업데이트

        if (Level >= 3)
        {
            Debug.Log("정상 동작 확인");
        }
    }

    private void UpdatePlanetSize()
    {
        if (Level != 1)
        {
            // 레벨에 맞춰 행성 크기 증가
            float newSize = planetTransform.localScale.x + sizeIncrement;
            planetTransform.localScale = new Vector3(newSize, newSize, 1f);
        }
        else
        {
            // 기본 크기 설정
            planetTransform.transform.localScale = new Vector3(0.4f, 0.4f, 1f);
        }
    }
}

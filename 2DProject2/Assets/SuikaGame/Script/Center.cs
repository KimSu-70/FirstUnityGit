using System.Collections.Generic;
using UnityEngine;

public class Center : MonoBehaviour
{
    [SerializeField] private float gravityStrength = 1.0f; // 중력 세기
    [SerializeField] private float gravityRange = 5.0f; // 중력장 범위
    [SerializeField] private float resistanceFactor = 0.5f; // 감쇄율

    private List<Rigidbody2D> planetsInRange = new List<Rigidbody2D>(); // 중력장 범위에 들어온 행성 목록

    private void Start()
    {
        // 중력장 콜라이더 설정
        CircleCollider2D gravityField = gameObject.AddComponent<CircleCollider2D>();
        gravityField.isTrigger = true; // 트리거로 설정
        gravityField.radius = gravityRange; // 중력 범위 설정
    }

    private void Update()
    {
        ApplyGravityToPlanetsInRange(); // 매 프레임마다 중력 적용
    }

    private void ApplyGravityToPlanetsInRange()
    {
        foreach (Rigidbody2D planetRb in planetsInRange)
        {
            if (planetRb)
            {
                ApplyGravity(planetRb); // 중력 적용
            }
        }
    }

    private void ApplyGravity(Rigidbody2D planetRb)
    {
        Vector2 direction = (Vector2)transform.position - planetRb.position;
        float distance = direction.magnitude; // 거리 계산

        // 중앙에 너무 가까우면 중력 작용 안 함
        if (distance < 1.0f) return; // 최소 거리 조정

        // 중력의 크기를 거리의 제곱에 반비례로 계산
        float forceMagnitude = gravityStrength / Mathf.Pow(distance, 2); // 거리 제곱 사용

        Vector2 gravityForce = direction.normalized * forceMagnitude; // 중력 벡터 계산
        Vector2 resistanceForce = -planetRb.velocity * resistanceFactor; // 저항력 계산

        planetRb.AddForce(gravityForce + resistanceForce); // 최종 힘 적용
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Rigidbody2D planetRb = other.GetComponent<Rigidbody2D>();

        if (planetRb && !planetsInRange.Contains(planetRb))
        {
            planetsInRange.Add(planetRb); // 리스트에 추가
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Rigidbody2D planetRb = other.GetComponent<Rigidbody2D>();

        if (planetRb)
        {
            planetsInRange.Remove(planetRb); // 리스트에서 제거
        }
    }
}

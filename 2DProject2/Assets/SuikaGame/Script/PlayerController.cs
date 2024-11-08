using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject planetPrefab; // 발사할 행성 프리팹
    [SerializeField] private float moveSpeed; // 행성 발사 속도
    [SerializeField] private float maxDistance; // 최대 이동 거리

    private GameObject currentPlanet; // 현재 발사된 행성

    private void Update()
    {
        RotateToMouse(); // 마우스 위치에 따라 회전

        // 마우스 클릭 시 행성 발사
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            LaunchPlanet();
        }

        // 현재 행성이 존재할 경우 거리 체크
        if (currentPlanet)
        {
            CheckPlanetDistance();
        }
    }

    // 마우스 위치에 따라 플레이어 회전
    private void RotateToMouse()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(mousePosition.y - transform.position.y, mousePosition.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    // 행성 발사
    private void LaunchPlanet()
    {
        currentPlanet = Instantiate(planetPrefab, transform.position, transform.rotation);
        Rigidbody2D rb2D = currentPlanet.GetComponent<Rigidbody2D>();
        rb2D.gravityScale = 0; // 중력 무효화
        rb2D.AddForce(transform.right * moveSpeed, ForceMode2D.Impulse); // 힘을 가해 발사
    }

    // 최대 거리 체크
    private void CheckPlanetDistance()
    {
        if (Vector2.Distance(currentPlanet.transform.position, transform.position) > maxDistance)
        {
            Destroy(currentPlanet); // 최대 거리 초과 시 삭제
        }
    }
}
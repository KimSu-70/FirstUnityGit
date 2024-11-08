using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject planetPrefab; // �߻��� �༺ ������
    [SerializeField] private float moveSpeed; // �༺ �߻� �ӵ�
    [SerializeField] private float maxDistance; // �ִ� �̵� �Ÿ�

    private GameObject currentPlanet; // ���� �߻�� �༺

    private void Update()
    {
        RotateToMouse(); // ���콺 ��ġ�� ���� ȸ��

        // ���콺 Ŭ�� �� �༺ �߻�
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            LaunchPlanet();
        }

        // ���� �༺�� ������ ��� �Ÿ� üũ
        if (currentPlanet)
        {
            CheckPlanetDistance();
        }
    }

    // ���콺 ��ġ�� ���� �÷��̾� ȸ��
    private void RotateToMouse()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(mousePosition.y - transform.position.y, mousePosition.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    // �༺ �߻�
    private void LaunchPlanet()
    {
        currentPlanet = Instantiate(planetPrefab, transform.position, transform.rotation);
        Rigidbody2D rb2D = currentPlanet.GetComponent<Rigidbody2D>();
        rb2D.gravityScale = 0; // �߷� ��ȿȭ
        rb2D.AddForce(transform.right * moveSpeed, ForceMode2D.Impulse); // ���� ���� �߻�
    }

    // �ִ� �Ÿ� üũ
    private void CheckPlanetDistance()
    {
        if (Vector2.Distance(currentPlanet.transform.position, transform.position) > maxDistance)
        {
            Destroy(currentPlanet); // �ִ� �Ÿ� �ʰ� �� ����
        }
    }
}
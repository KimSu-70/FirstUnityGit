using System.Collections.Generic;
using UnityEngine;

public class Center : MonoBehaviour
{
    [SerializeField] private float gravityStrength = 1.0f; // �߷� ����
    [SerializeField] private float gravityRange = 5.0f; // �߷��� ����
    [SerializeField] private float resistanceFactor = 0.5f; // ������

    private List<Rigidbody2D> planetsInRange = new List<Rigidbody2D>(); // �߷��� ������ ���� �༺ ���

    private void Start()
    {
        // �߷��� �ݶ��̴� ����
        CircleCollider2D gravityField = gameObject.AddComponent<CircleCollider2D>();
        gravityField.isTrigger = true; // Ʈ���ŷ� ����
        gravityField.radius = gravityRange; // �߷� ���� ����
    }

    private void Update()
    {
        ApplyGravityToPlanetsInRange(); // �� �����Ӹ��� �߷� ����
    }

    private void ApplyGravityToPlanetsInRange()
    {
        foreach (Rigidbody2D planetRb in planetsInRange)
        {
            if (planetRb)
            {
                ApplyGravity(planetRb); // �߷� ����
            }
        }
    }

    private void ApplyGravity(Rigidbody2D planetRb)
    {
        Vector2 direction = (Vector2)transform.position - planetRb.position;
        float distance = direction.magnitude; // �Ÿ� ���

        // �߾ӿ� �ʹ� ������ �߷� �ۿ� �� ��
        if (distance < 1.0f) return; // �ּ� �Ÿ� ����

        // �߷��� ũ�⸦ �Ÿ��� ������ �ݺ�ʷ� ���
        float forceMagnitude = gravityStrength / Mathf.Pow(distance, 2); // �Ÿ� ���� ���

        Vector2 gravityForce = direction.normalized * forceMagnitude; // �߷� ���� ���
        Vector2 resistanceForce = -planetRb.velocity * resistanceFactor; // ���׷� ���

        planetRb.AddForce(gravityForce + resistanceForce); // ���� �� ����
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Rigidbody2D planetRb = other.GetComponent<Rigidbody2D>();

        if (planetRb && !planetsInRange.Contains(planetRb))
        {
            planetsInRange.Add(planetRb); // ����Ʈ�� �߰�
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Rigidbody2D planetRb = other.GetComponent<Rigidbody2D>();

        if (planetRb)
        {
            planetsInRange.Remove(planetRb); // ����Ʈ���� ����
        }
    }
}

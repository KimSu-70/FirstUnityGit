using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TankMonster : MonoBehaviour
{
    public int maxHealth = 3; // �⺻ ü��
    [SerializeField] Transform target;
    private int currentHealth;

    public float spawnPoint;
    public float Y;

    private void Start()
    {
        // �ʱ� ü�� ����
        currentHealth = maxHealth;
    }

    private void Update()
    {
        transform.LookAt(target.position);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // ü�� ����
        if (currentHealth <= 0)
        {
            Respawn(); // ü���� 0 ���Ϸ� �������� ������
        }
    }

    public void Respawn()
    {
        Vector3 point = new Vector3
            (
                Random.Range(transform.position.x - spawnPoint, transform.position.x + spawnPoint),
                Y,
                Random.Range(transform.position.z - spawnPoint, transform.position.z + spawnPoint)
            );
        transform.position = point;
        currentHealth = maxHealth; // ü���� �⺻ ������ ȸ��
    }
}

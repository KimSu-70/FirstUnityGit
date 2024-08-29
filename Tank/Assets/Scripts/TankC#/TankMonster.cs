using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TankMonster : MonoBehaviour
{
    public int maxHealth = 3; // 기본 체력
    [SerializeField] Transform target;
    private int currentHealth;

    public float spawnPoint;
    public float Y;

    private void Start()
    {
        // 초기 체력 설정
        currentHealth = maxHealth;
    }

    private void Update()
    {
        transform.LookAt(target.position);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // 체력 감소
        if (currentHealth <= 0)
        {
            Respawn(); // 체력이 0 이하로 떨어지면 리스폰
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
        currentHealth = maxHealth; // 체력을 기본 값으로 회복
    }
}

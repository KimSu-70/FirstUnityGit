using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsMonster : MonoBehaviour
{
    public int maxHealth = 3; // 기본 체력
    private int currentHealth;
    
    [SerializeField] Transform target;

    [SerializeField] float monsterSpeed;

    public float spawnPoint;
    public float Y;

    private Coroutine Respawns;

    private void Start()
    {
        // 초기 체력 설정
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (gameObject.activeSelf) // activeSelf : GameObject가 현재 활성화되어 있는지 여부를 확인
        {
            transform.LookAt(target.position);
            transform.position = Vector3.MoveTowards(transform.position, target.position, monsterSpeed);
        }
    }

    public void TakeDamage(int damage)
    {
        if (!gameObject.activeSelf)
        { return; }
        currentHealth -= damage; // 체력 감소
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        MonsterSwitch.Instance.Deactivate(gameObject);

        if (Respawns != null)
        {
            StopCoroutine(Respawns);
        }
        Respawns = StartCoroutine(StartRespawn());
    }
    IEnumerator StartRespawn()
    {
        Debug.Log("몬스터가 사망했습니다.");
        yield return new WaitForSeconds(3f);
        Respawn();
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
        MonsterSwitch.Instance.Activate(gameObject);
    }
}

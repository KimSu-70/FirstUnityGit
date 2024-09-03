using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsMonster : MonoBehaviour
{
    public int maxHealth = 3; // �⺻ ü��
    private int currentHealth;
    
    [SerializeField] Transform target;

    [SerializeField] float monsterSpeed;

    public float spawnPoint;
    public float Y;

    private Coroutine Respawns;

    private void Start()
    {
        // �ʱ� ü�� ����
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (gameObject.activeSelf) // activeSelf : GameObject�� ���� Ȱ��ȭ�Ǿ� �ִ��� ���θ� Ȯ��
        {
            transform.LookAt(target.position);
            transform.position = Vector3.MoveTowards(transform.position, target.position, monsterSpeed);
        }
    }

    public void TakeDamage(int damage)
    {
        if (!gameObject.activeSelf)
        { return; }
        currentHealth -= damage; // ü�� ����
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
        Debug.Log("���Ͱ� ����߽��ϴ�.");
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
        currentHealth = maxHealth; // ü���� �⺻ ������ ȸ��
        MonsterSwitch.Instance.Activate(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    [SerializeField] Transform target;      // �Ѿ� Ÿ��
    [SerializeField] Transform muzzlePoint;     //�Ѿ� ���� ��ġ

    // ������ : ���ӿ�����Ʈ ���赵 -> ����Ƽ���� ���ӿ�����Ʈ�� �����Ҷ� ������ ����
    [SerializeField] GameObject bulletPrefab;     // ������ �Ѿ� ������
    [SerializeField] float bulletTime;  // �Ѿ� ���� �ֱ�
    [SerializeField] float remainTime;  // ���� �Ѿ� ���� �Ҷ����� ��ٸ��ð�
    [SerializeField] bool isAttacking;  // ���� ����

    [Range(1, 10)]
    [SerializeField] float bulletSpeed; //�Ѿ� �ӵ�

    private void Start()
    {
        // GameObject.FindGameObjectWithTag : ���ӿ� �ִ� �±׸� ���ؼ� Ư�� ���ӿ�����Ʈ�� ã��
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        // GetComponent : ���ӿ�����Ʈ�� �ִ� ������Ʈ ��������
        target = playerObj.GetComponent<Transform>();   //1��

        // transform : ���ӿ�����Ʈ�� ��ġ, ȸ��, ũ�⸦ �����ϴ� ��ɴ����
        // transform ������Ʈ�� ��� ���ӿ�����Ʈ�� �ݵ�� ���� -> transform ������Ʈ�� Ư���ϰ� transform ������Ƽ�� �ٷ� ��� ����
        target = playerObj.transform;                   //2��     1,2�� �Ѵ� ������

    }

    private void Update()
    {
        transform.LookAt(target.position);


        // ���� ���� �ƴҶ� �Ѿ˻����� ���� �ʵ��� ��
        if (isAttacking == false)
            return;

        // ���� �Ѿ� �������� ���� �ð��� ��� ����
        remainTime -= Time.deltaTime;

        // ���� �Ѿ��� �����Ҷ����� ���� �ð��� ���� ��� == �Ѿ��� ������ Ÿ�̹�
        if (remainTime <= 0)
        {
            // bulletPrefab ���赵�� ���� �Ѿ��� ����
            // Instantiate : �������� ���� ���ӿ�����Ʈ �����ϱ�
            // Instantiate(������, ��ġ, ȸ��);
            GameObject bulletGameObj = Instantiate(bulletPrefab, muzzlePoint.position, muzzlePoint.rotation);
            Bullet bullet = bulletGameObj.GetComponent<Bullet>();
            bullet.SetTarget(target);

            // ���� �Ѿ��� �����Ҷ����� ���� �ð��� �ٽ� ����
            remainTime = bulletTime;
        }
    }
    
    public void StartAttack()
    {
        isAttacking = true;
    }

    public void StopAttack()
    {
        isAttacking = false;
    }
}

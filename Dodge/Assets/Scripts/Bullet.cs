using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody rigid;
    [SerializeField] Transform target;
    [SerializeField] float speed;

    private void Start()
    {
        // �Ѿ��� Ÿ���� �ٶ���� ��
        // LookAt : Ÿ�� ������ �ٶ󺸴� ȸ��
        transform.LookAt(target.position);

        // �Ѿ��� �ӵ��� �չ��� speed ��ŭ���� ����
        rigid.velocity = transform.forward * speed;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }


    // �浹(Collision) : ������ �浹�� Ȯ��
    // Ʈ����(Trigger) : ��ħ���� Ȯ��
    // ����Ƽ �浹 �޽��� �Լ�
    private void OnCollisionEnter(Collision collision)
    {
        // Collision �Ű����� : �浹�� ��Ȳ�� ���� �������� ������ �ִ�(ex, �浹�� �ٸ� �浹ü, ���� ��, �ε��� ����, ��)
        if(collision.gameObject.tag == "Player")
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController.TakeHit();
            Destroy(gameObject);
        }
        else
        {
            // Destroy : ���ӿ�����Ʈ ����
            Destroy(gameObject);
        }
    }

}

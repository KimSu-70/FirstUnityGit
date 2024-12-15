using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class UniRXTester : MonoBehaviour
{
    [SerializeField] Rigidbody rigid;

    [SerializeField] bool isGround;


    private void Start()
    {
        // ������Ʈ���� ������ �������� ����
        this.UpdateAsObservable()

        // ���������� ������ �� ���ǿ� �´� ��츸 �˷���
        .Where(x => Input.GetKeyDown(KeyCode.Space))
        .Where(x => isGround == true)
        // ���� �۾��� �ʿ��� �����͸� �Ѱ��� (���� ��Ȳ�� �ʿ� ����)
        .Select(x => transform.position)

        // �������� �˷��ٶ����� ������ �Լ�(�ൿ)�� ��������
        .Subscribe(x => { rigid.velocity = Vector3.up * 5f; });


        // ���� �ε����� ���� ��� Ȯ���� stream�� ���� �ε����� ��Ȳ���� isGround�� true�� �������
        this.OnCollisionEnterAsObservable()
            .Where(x => x.gameObject.layer == LayerMask.NameToLayer("Ground"))
            .Subscribe(x => isGround = true);

        // ���� �������� ���� ��� Ȯ���� stream�� ���� �������� ��Ȳ���� isGround�� false�� �������
        this.OnCollisionExitAsObservable()
            .Where(x => x.gameObject.layer == LayerMask.NameToLayer("Ground"))
            .Subscribe(x => isGround = false);

        // �̵� ���� stream
        //this.UpdateAsObservable()
        //    .Subscribe(param =>
        //    {
        //        float x = Input.GetAxisRaw("Horizontal");
        //        float z = Input.GetAxisRaw("Vertical");

        //        Vector3 velocity = rigid.velocity;
        //        velocity.x = x;
        //        velocity.z = z;

        //        rigid.velocity = velocity;
        //    });
    }

    private void Update()
    {
        
    }

    private void Jump()
    {
        rigid.velocity = Vector3.up * 5f;
    }
}

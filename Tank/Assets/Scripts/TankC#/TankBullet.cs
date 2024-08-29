using UnityEngine;

public class TankBullet : MonoBehaviour
{
    [SerializeField] Rigidbody rigid;
    public int attackPower;
    public float speed;


    private void Start()
    {
        Destroy(gameObject, 4f);

        rigid.velocity = transform.forward * speed;
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Monster")
        {
            TankMonster monster = collision.gameObject.GetComponent<TankMonster>();
            monster.TakeDamage(attackPower); // ���Ϳ��� ������ �ο�
            Destroy(gameObject); // źȯ ����
        }
    }
}

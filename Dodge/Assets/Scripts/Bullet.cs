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
        // 총알이 타켓을 바라봐야 함
        // LookAt : 타켓 방향을 바라보는 회전
        transform.LookAt(target.position);

        // 총알의 속도를 앞방향 speed 만큼으로 결정
        rigid.velocity = transform.forward * speed;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }


    // 충돌(Collision) : 물리적 충돌을 확인
    // 트리거(Trigger) : 겹침여부 확인
    // 유니티 충돌 메시지 함수
    private void OnCollisionEnter(Collision collision)
    {
        // Collision 매개변수 : 충돌한 상황에 대한 정보들을 가지고 있다(ex, 충돌한 다른 충돌체, 받은 힘, 부딪힌 지점, 등)
        if(collision.gameObject.tag == "Player")
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController.TakeHit();
            Destroy(gameObject);
        }
        else
        {
            // Destroy : 게임오브젝트 삭제
            Destroy(gameObject);
        }
    }

}

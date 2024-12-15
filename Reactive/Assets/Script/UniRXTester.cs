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
        // 업데이트마다 반응할 옵저버를 만듬
        this.UpdateAsObservable()

        // 옵저버에게 반응할 때 조건에 맞는 경우만 알려줌
        .Where(x => Input.GetKeyDown(KeyCode.Space))
        .Where(x => isGround == true)
        // 다음 작업에 필요한 데이터를 넘겨줌 (지금 상황에 필요 없음)
        .Select(x => transform.position)

        // 옵저버가 알려줄때마다 실행할 함수(행동)를 연결해줌
        .Subscribe(x => { rigid.velocity = Vector3.up * 5f; });


        // 땅과 부딪히는 것을 계속 확인할 stream이 땅과 부딪히는 상황에서 isGround를 true로 만들어줌
        this.OnCollisionEnterAsObservable()
            .Where(x => x.gameObject.layer == LayerMask.NameToLayer("Ground"))
            .Subscribe(x => isGround = true);

        // 땅과 떨어지는 것을 계속 확인할 stream이 땅과 떨어지는 상황에서 isGround를 false로 만들어줌
        this.OnCollisionExitAsObservable()
            .Where(x => x.gameObject.layer == LayerMask.NameToLayer("Ground"))
            .Subscribe(x => isGround = false);

        // 이동 전용 stream
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

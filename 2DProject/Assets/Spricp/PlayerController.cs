using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] SpriteRenderer render;
    [SerializeField] Animator animator;
    [SerializeField] float movePower;
    [SerializeField] float MaxSpeed;
    [SerializeField] float jumpPower;
    [SerializeField] float maxFallSpeed;
    [SerializeField] bool isGrounded;
    [SerializeField] LayerMask mask;

    private float x;

    private static int run = Animator.StringToHash("run");
    private static int idle = Animator.StringToHash("idle");
    private static int jumpup = Animator.StringToHash("jumpUp");
    private static int jumpDown = Animator.StringToHash("JumpDown");

    private int curAniHash;

    private void Update()
    {
        x = Input.GetAxisRaw("Horizontal");
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        GroundCheck();
        AnimatorPlay();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        rigid.AddForce(Vector2.right * x * movePower, ForceMode2D.Force);
        if(rigid.velocity.x > MaxSpeed)
        {
            rigid.velocity = new Vector2(MaxSpeed, rigid.velocity.y);
        }
        else if(rigid.velocity.x < -MaxSpeed)
        {
            rigid.velocity = new Vector2(-MaxSpeed, rigid.velocity.y);
        }


        if(rigid.velocity.y < -maxFallSpeed)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, -maxFallSpeed);
        }


        //if(rigid.velocity.sqrMagnitude < 0.01)
        //{
        //    animator.Play(idle);
        //}
        //else
        //{
        //    animator.Play(run);
        //}


        if (x < 0)
        {
            render.flipX = true;
        }

        else if (x > 0)
        {
            render.flipX = false;
        }
    }

    private void Jump()
    {
        if (isGrounded == false)
            return;

        rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
    }

    private void GroundCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.2f, mask);
        if (hit.collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded= false;
        }
    }
    private void AnimatorPlay()
    {
        int checkAniHash;
        if(rigid.velocity.y > 0.01f)
        {
            checkAniHash = jumpup;
        }
        else if (rigid.velocity.y < -0.01f)
        {
            checkAniHash = jumpDown;
        }
        else if(rigid.velocity.sqrMagnitude < 0.01f)
        {
            checkAniHash = idle;
        }
        else
        {
            checkAniHash = run;
        }

        if (curAniHash != checkAniHash)
            curAniHash = checkAniHash;
            animator.Play(curAniHash);
    }
}

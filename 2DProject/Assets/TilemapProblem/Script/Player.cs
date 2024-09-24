using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Networking.Types;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // 상태 패턴
    public enum State { Idle, Run, JumpUp, JumpDown, Attack, Dead, Size }
    [SerializeField] State curState = State.Idle;
    private BaseState[] states = new BaseState[(int)State.Size];


    [SerializeField] Vector2 startPos;
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject players;

    [Header("UI")]
    [SerializeField]
    Image[] imageHp;
    [SerializeField] TextMeshProUGUI textCoin;

    [SerializeField] public int hp = 3;
    [SerializeField] private int coinCount = 0;
    

    private void Awake()
    {
        states[(int)State.Idle] = new IdleState(this);
        states[(int)State.Run] = new RunState(this);
        states[(int)State.JumpUp] = new JumpUpState(this);
        states[(int)State.JumpDown] = new JumpDownState(this);
        states[(int)State.Attack] = new AttackState(this);
        states[(int)State.Dead] = new DeadState(this);
    }

    private void Start()
    {
        startPos = transform.position;

        states[(int)curState].Enter();
    }

    private void OnDestroy()
    {
        states[(int)curState].Exit();
    }

    private void Update()
    {
        states[(int)curState].Update();
    }

    public void ChangeState(State nextState)
    {
        if (curState != nextState)
        {
            states[(int)curState].Exit();
            curState = nextState;
            states[(int)curState].Enter();
        }
    }

    public void TakeDamage(int damage)
    {
        hp -= damage; // 체력 감소
        HpUI();

        if (hp <= 0)
        {
            ChangeState(State.Dead);
        }
    }

    private void HpUI()
    {
        for (int i = 0; i < imageHp.Length; i++)
        {
            // 각 하트 이미지의 활성화 상태를 업데이트
            if (i < hp)
            {
                imageHp[i].enabled = true; // 체력이 남아있으면 하트 이미지 활성화
            }
            else
            {
                imageHp[i].enabled = false; // 체력이 없으면 하트 이미지 비활성화
            }
        }
    }

    public void CollectCoin()
    {
        coinCount++; // 코인 수 증가
        CoinUI();
    }

    private void CoinUI()
    {
        textCoin.text = coinCount.ToString();
    }

    private class PlayerState : BaseState
    {
        public Player player;

        public PlayerState(Player player)
        {
            this.player = player;
        }
    }

    private class IdleState : PlayerState
    {
        public IdleState(Player player) : base(player)
        {
        }

        public override void Enter()
        {
            player.animator.Play("Idles");
        }

        public override void Update()
        {
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                player.ChangeState(State.Run);
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                player.ChangeState(State.JumpUp);
            }
        }
    }

    private class RunState : PlayerState
    {
        public RunState(Player player) : base(player)
        {
        }

        public override void Enter()
        {
            player.animator.Play("Runs");
        }

        public override void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                player.ChangeState(State.JumpUp);
            }
        }
    }

    private class JumpUpState : PlayerState
    {
        public JumpUpState(Player player) : base(player)
        {
        }

        public override void Enter()
        {
            player.animator.Play("JumpUP");
        }

        public override void Update()
        {
            if (player.rigid.velocity.y < -0.1f) // 하강 중
            {
                player.ChangeState(State.JumpDown);
            }
        }
    }

    private class JumpDownState : PlayerState
    {
        public JumpDownState(Player player) : base(player)
        {
        }

        public override void Enter()
        {
            player.animator.Play("JumpDOWN");
        }

        public override void Update()
        {
            if (player.rigid.velocity.y > 0)
            {
                player.ChangeState(State.Idle);
            }
        }
    }

    private class AttackState : PlayerState
    {
        public AttackState(Player player) : base(player)
        {
        }

        public override void Update()
        {
            
        }
    }

    private class DeadState : PlayerState
    {
        public DeadState(Player player) : base(player)
        {
        }

        public override void Enter()
        {
            player.animator.Play("Hurt");
            player.StartCoroutine(IGameOver(1f));
            Destroy(player.players, 2f);
        }

        public override void Exit()
        {
            player.StopCoroutine(IGameOver(0f));
        }

        private IEnumerator IGameOver(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            player.gameManager.GameOver();
        }
    }
}

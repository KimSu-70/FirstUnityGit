using System.Collections;
using UnityEngine;

public class Eagle : MonoBehaviour
{
    // 상태 패턴
    public enum State { Idle, Trace, Return, Attack, Dead, Size }
    [SerializeField] State curState = State.Idle;
    private BaseState[] states = new BaseState[(int)State.Size];

    [SerializeField] GameObject player;
    [SerializeField] GameObject eagles;
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer render;
    [SerializeField] Player playerhit;

    [SerializeField] float traceRange;
    [SerializeField] float attackRange;
    [SerializeField] float moveSpeed;
    [SerializeField] Vector2 startPos;

    #region 싱글패턴1
    //// [SerializeField] Text stateText;

    //private void Start()
    //{
    //    startPos = transform.position;
    //    player = GameObject.FindGameObjectWithTag("Player");
    //}

    //private void Update()
    //{
    //    switch (curState)
    //    {
    //        case State.Idle:
    //            Idle();
    //            break;
    //        case State.Trace:
    //            Trace();
    //            break;
    //        case State.Return:
    //            Return();
    //            break;
    //        case State.Attack:
    //            Attack();
    //            break;
    //        case State.Dead:
    //            Dead();
    //            break;
    //    }

    //    // stateText.text = curState.ToString();
    //}

    //private void Idle()
    //{
    //    // Idle 행동만 구현
    //    // 가만히 있기

    //    // 다른 상태로 전환
    //    if (Vector2.Distance(transform.position, player.transform.position) < traceRange)
    //    {
    //        curState = State.Trace;
    //    }
    //}

    //private void Trace()
    //{
    //    // Trace 행동만 구현
    //    transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);

    //    // 다른 상태로 전환
    //    if (Vector2.Distance(transform.position, player.transform.position) > traceRange)
    //    {
    //        curState = State.Return;
    //    }
    //    else if (Vector2.Distance(transform.position, player.transform.position) < attackRange)
    //    {
    //        curState = State.Attack;
    //    }
    //}

    //private void Return()
    //{
    //    // Return 행동만 구현
    //    transform.position = Vector2.MoveTowards(transform.position, startPos, moveSpeed * Time.deltaTime);

    //    if (Vector2.Distance(transform.position, startPos) < 0.01f)
    //    {
    //        curState = State.Idle;
    //    }
    //}

    //private void Attack()
    //{
    //    // Attack 행동만 구현
    //    Debug.Log("공격");

    //    if (Vector2.Distance(transform.position, player.transform.position) > attackRange)
    //    {
    //        curState = State.Trace;
    //    }
    //}

    //private void Dead()
    //{
    //    // Dead 행동만 구현
    //    Debug.Log("죽음");
    //}
    #endregion
    private void Awake()
    {
        states[(int)State.Idle] = new IdleState(this);
        states[(int)State.Trace] = new TraceState(this);
        states[(int)State.Return] = new ReturnState(this);
        states[(int)State.Attack] = new AttackState(this);
        states[(int)State.Dead] = new DeadState(this);
    }

    private void Start()
    {
        startPos = transform.position;
        player = GameObject.FindGameObjectWithTag("Player");

        states[(int)curState].Enter();
    }

    private void OnDestroy()
    {
        states[(int)curState].Exit();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // 플레이어와 충돌했을 때
        {
            ChangeState(State.Dead);
        }
    }

    private void Update()
    {
        states[(int)curState].Update();
        //stateText.text = curState.ToString();
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

    private class EagleState : BaseState
    {
        public Eagle eagle;

        public EagleState(Eagle eagle)
        {
            this.eagle = eagle;
        }
    }

    private class IdleState : EagleState
    {
        public IdleState(Eagle eagle) : base(eagle)
        {
        }

        public override void Enter()
        {
            eagle.animator.Play("Idle");
        }

        public override void Update()
        {
            // Idle 행동만 구현
            // 가만히 있기
            // 다른 상태로 전환
            if (Vector2.Distance(eagle.transform.position, eagle.player.transform.position) < eagle.traceRange)
            {
                eagle.ChangeState(State.Trace);
            }
        }
    }

    private class TraceState : EagleState
    {
        public TraceState(Eagle eagle) : base(eagle)
        {
        }

        public override void Enter()
        {
            eagle.animator.Play("Idle");
        }

        public override void Update()
        {
            Vector2 direction = (eagle.player.transform.position - eagle.transform.position).normalized;
            eagle.transform.position = Vector2.MoveTowards(eagle.transform.position, eagle.player.transform.position, eagle.moveSpeed * Time.deltaTime);

            // 이동 방향에 따라 방향 반전
            if (direction.x < 0)
            {
                eagle.render.flipX = false;
            }
            else if (direction.x > 0)
            {
                eagle.render.flipX = true;
            }

            // 다른 상태로 전환
            if (Vector2.Distance(eagle.transform.position, eagle.player.transform.position) > eagle.traceRange)
            {
                eagle.ChangeState(State.Return);
            }
            else if (Vector2.Distance(eagle.transform.position, eagle.player.transform.position) < eagle.attackRange)
            {
                eagle.ChangeState(State.Attack);
            }
        }
    }

    private class ReturnState : EagleState
    {
        public ReturnState(Eagle eagle) : base(eagle)
        {
        }

        public override void Update()
        {
            // Return 행동만 구현
            eagle.transform.position = Vector2.MoveTowards(eagle.transform.position, eagle.startPos, eagle.moveSpeed * Time.deltaTime);

            if (Vector2.Distance(eagle.transform.position, eagle.startPos) < 0.01f)
            {
                eagle.ChangeState(State.Idle);
            }
        }
    }

    private class AttackState : EagleState
    {
        public AttackState(Eagle eagle) : base(eagle)
        {
        }

        public override void Enter()
        {
            eagle.animator.Play("Attatc");
            eagle.AttackPlayer();
        }

        public override void Update()
        {
            // Attack 행동만 구현
            if (Vector2.Distance(eagle.transform.position, eagle.player.transform.position) > eagle.attackRange)
            {
                eagle.ChangeState(State.Trace);
            }
        }
    }

    private void AttackPlayer()
    {
        playerhit.TakeDamage(1);
    }
    
    

    private class DeadState : EagleState
    {
        public DeadState(Eagle eagle) : base(eagle)
        {
        }
        public override void Enter()
        {
            eagle.animator.Play("Dead");
            Destroy(eagle.eagles, 0.5f);
        }

        public override void Exit()
        {
            
        }
    }


    #region BaseStatePattern
    /*
    private void Idle()
    {
        // Idle 행동만 구현
        // 가만히 있기

        // 다른 상태로 전환
        if (Vector2.Distance(transform.position, player.transform.position) < traceRange)
        {
            curState = State.Trace;
        }
    }

    private void Trace()
    {
        // Trace 행동만 구현
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);

        // 다른 상태로 전환
        if (Vector2.Distance(transform.position, player.transform.position) > traceRange)
        {
            curState = State.Return;
        }
        else if (Vector2.Distance(transform.position, player.transform.position) < attackRange)
        {
            curState = State.Attack;
        }
    }

    private void Return()
    {
        // Return 행동만 구현
        transform.position = Vector2.MoveTowards(transform.position, startPos, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, startPos) < 0.01f)
        {
            curState = State.Idle;
        }
    }

    private void Attack()
    {
        // Attack 행동만 구현
        Debug.Log("공격");

        if (Vector2.Distance(transform.position, player.transform.position) > attackRange)
        {
            curState = State.Trace;
        }
    }

    private void Dead()
    {
        // Dead 행동만 구현
        Debug.Log("죽음");
    }
    */
    #endregion
}



//using UnityEngine;
//using UnityEngine.UI;

//public class Eagle : MonoBehaviour
//{
//    public enum State { Idle, Trace, Return, Attack, Dead, Size }

//    [Header("State")]
//    [SerializeField] State curState = State.Idle;

//    private BaseState[] states = new BaseState[(int)State.Size];
//    [SerializeField] IdleState idleState;
//    [SerializeField] TraceState traceState;
//    [SerializeField] ReturnState returnState;
//    [SerializeField] AttackState attackState;
//    [SerializeField] DeadState deadState;

//    [Header("Property")]
//    [SerializeField] GameObject player;
//    [SerializeField] Vector2 startPos;

//    [SerializeField] Text stateText;

//    private void Awake()
//    {
//        states[(int)State.Idle] = idleState;
//        states[(int)State.Trace] = traceState;
//        states[(int)State.Return] = returnState;
//        states[(int)State.Attack] = attackState;
//        states[(int)State.Dead] = deadState;
//    }

//    private void Start()
//    {
//        startPos = transform.position;
//        player = GameObject.FindGameObjectWithTag("Player");

//        states[(int)curState].Enter();
//    }

//    private void OnDestroy()
//    {
//        states[(int)curState].Exit();
//    }

//    private void Update()
//    {
//        states[(int)curState].Update();

//        stateText.text = curState.ToString();
//    }

//    public void ChangeState(State nextState)
//    {
//        states[(int)curState].Exit();
//        curState = nextState;
//        states[(int)curState].Enter();
//    }

//    [System.Serializable]
//    private class IdleState : BaseState
//    {
//        [SerializeField] Eagle eagle;
//        [SerializeField] float traceRange;

//        public override void Update()
//        {
//            // Idle 행동만 구현
//            // 가만히 있기

//            // 다른 상태로 전환
//            if (Vector2.Distance(eagle.transform.position, eagle.player.transform.position) < traceRange)
//            {
//                eagle.ChangeState(State.Trace);
//            }
//        }
//    }

//    [System.Serializable]
//    private class TraceState : BaseState
//    {
//        [SerializeField] Eagle eagle;
//        [SerializeField] float traceRange;
//        [SerializeField] float attackRange;
//        [SerializeField] float moveSpeed;

//        public override void Update()
//        {
//            // Trace 행동만 구현
//            eagle.transform.position = Vector2.MoveTowards(eagle.transform.position, eagle.player.transform.position, moveSpeed * Time.deltaTime);

//            // 다른 상태로 전환
//            if (Vector2.Distance(eagle.transform.position, eagle.player.transform.position) > traceRange)
//            {
//                eagle.ChangeState(State.Return);
//            }
//            else if (Vector2.Distance(eagle.transform.position, eagle.player.transform.position) < attackRange)
//            {
//                eagle.ChangeState(State.Attack);
//            }
//        }
//    }

//    [System.Serializable]
//    private class ReturnState : BaseState
//    {
//        [SerializeField] Eagle eagle;
//        [SerializeField] float moveSpeed;

//        public override void Update()
//        {
//            // Return 행동만 구현
//            eagle.transform.position = Vector2.MoveTowards(eagle.transform.position, eagle.startPos, moveSpeed * Time.deltaTime);

//            if (Vector2.Distance(eagle.transform.position, eagle.startPos) < 0.01f)
//            {
//                eagle.ChangeState(State.Idle);
//            }
//        }
//    }

//    [System.Serializable]
//    private class AttackState : BaseState
//    {
//        [SerializeField] Eagle eagle;
//        [SerializeField] float attackRange;

//        public override void Update()
//        {
//            // Attack 행동만 구현
//            Debug.Log("공격");

//            if (Vector2.Distance(eagle.transform.position, eagle.player.transform.position) > attackRange)
//            {
//                eagle.ChangeState(State.Trace);
//            }
//        }
//    }

//    [System.Serializable]
//    private class DeadState : BaseState
//    {
//        [SerializeField] Eagle eagle;
//    }


    #region BaseStatePattern
    /*
    private void Idle()
    {
        // Idle 행동만 구현
        // 가만히 있기

        // 다른 상태로 전환
        if (Vector2.Distance(transform.position, player.transform.position) < traceRange)
        {
            curState = State.Trace;
        }
    }

    private void Trace()
    {
        // Trace 행동만 구현
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);

        // 다른 상태로 전환
        if (Vector2.Distance(transform.position, player.transform.position) > traceRange)
        {
            curState = State.Return;
        }
        else if (Vector2.Distance(transform.position, player.transform.position) < attackRange)
        {
            curState = State.Attack;
        }
    }

    private void Return()
    {
        // Return 행동만 구현
        transform.position = Vector2.MoveTowards(transform.position, startPos, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, startPos) < 0.01f)
        {
            curState = State.Idle;
        }
    }

    private void Attack()
    {
        // Attack 행동만 구현
        Debug.Log("공격");

        if (Vector2.Distance(transform.position, player.transform.position) > attackRange)
        {
            curState = State.Trace;
        }
    }

    private void Dead()
    {
        // Dead 행동만 구현
        Debug.Log("죽음");
    }
    */
    #endregion


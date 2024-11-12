using Cinemachine;
using Fusion;
using UnityEngine;

// 인스턴스 달아줄때 사용
// [RequireComponent(typeof(CharacterController))]
public class PlayerController : NetworkBehaviour
{
    [SerializeField] PlayerModel model;
    [SerializeField] Weapon weapon;
    [SerializeField] float curMoveSpeed;           // 현재 속도
    [SerializeField] float moveSpeed;              // 속도
    [SerializeField] float movesSpeed;             // 감속

    [SerializeField] float mouseX;
    [SerializeField] float mouseY;

    public bool isJump;

    public Vector3 move;

    [Header("Camera")]
    [SerializeField] private Transform playerCamera;
    private float verticalRotation = 0.0f; // 카메라의 수직 회전 각도
    [SerializeField] float sensitivity = 15.0f; // 마우스 감도
    public GameObject gunCanvas;
    [SerializeField] CinemachineVirtualCamera playerCam1;
    [SerializeField] CinemachineFreeLook playerCam2;



    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // 프레임마다 호출되는 함수
    // 개인 컴퓨터에서 처리할 작업
    private void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        mouseX = Input.GetAxis("Mouse X") * sensitivity;
        mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        move = transform.right * moveX + transform.forward * moveZ;
        if (Input.GetMouseButton(0))
        {
            if (HasStateAuthority == false)
                return;
            weapon.Fire();
        }
        if (Input.GetMouseButton(1))
        {
            gunCanvas.SetActive(true);
            playerCam2On();
        }
        else
        {
            gunCanvas.SetActive(false);
            playerCam2Off();
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            curMoveSpeed = movesSpeed;
        }
        else
        {
            curMoveSpeed = moveSpeed;
        }
    }

    // Rpc : 원격 함수 호출
    // RpcSources : 누가 보낼 수 있는지
    // RpcTargets : 누가한테 보낼 것 인지
    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void TakeHitRpc(int damage)
    {
        Debug.Log("피격");
        model.hp -= damage;
    }

    // 네트워크 통신 주기마다 호출
    // 네트워크에서 처리할 작업
    public override void FixedUpdateNetwork()
    {
        // 내 소유권의 네트워크 오브젝트가 아닌 경우
        if (HasStateAuthority == false)
            return;
        transform.position += move * curMoveSpeed * Time.deltaTime;

        Rotates();
    }

    // 네트워크 오브젝트가 생성됐을 때 호출됨
    //public override void Spawned()
    //{
    //    if (HasStateAuthority == true)
    //    {

    //    }
    //}

    private void Rotates()
    {
        // 플레이어 캐릭터의 수평 회전
        transform.Rotate(Vector3.up * mouseX);

        // 카메라의 수직 회전
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -80f, 80f);
        playerCamera.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
    }

    private void playerCam2On()
    {
        if (HasStateAuthority == false)
            return;
        playerCam1.Priority = 10;
        playerCam2.Priority = 20;
    }

    private void playerCam2Off()
    {
        if (HasStateAuthority == false)
            return;
        playerCam1.Priority = 20;
        playerCam2.Priority = 10;
    }
}
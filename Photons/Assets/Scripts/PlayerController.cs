using Cinemachine;
using Fusion;
using UnityEngine;

// �ν��Ͻ� �޾��ٶ� ���
// [RequireComponent(typeof(CharacterController))]
public class PlayerController : NetworkBehaviour
{
    [SerializeField] PlayerModel model;
    [SerializeField] Weapon weapon;
    [SerializeField] float curMoveSpeed;           // ���� �ӵ�
    [SerializeField] float moveSpeed;              // �ӵ�
    [SerializeField] float movesSpeed;             // ����

    [SerializeField] float mouseX;
    [SerializeField] float mouseY;

    public bool isJump;

    public Vector3 move;

    [Header("Camera")]
    [SerializeField] private Transform playerCamera;
    private float verticalRotation = 0.0f; // ī�޶��� ���� ȸ�� ����
    [SerializeField] float sensitivity = 15.0f; // ���콺 ����
    public GameObject gunCanvas;
    [SerializeField] CinemachineVirtualCamera playerCam1;
    [SerializeField] CinemachineFreeLook playerCam2;



    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // �����Ӹ��� ȣ��Ǵ� �Լ�
    // ���� ��ǻ�Ϳ��� ó���� �۾�
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

    // Rpc : ���� �Լ� ȣ��
    // RpcSources : ���� ���� �� �ִ���
    // RpcTargets : �������� ���� �� ����
    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void TakeHitRpc(int damage)
    {
        Debug.Log("�ǰ�");
        model.hp -= damage;
    }

    // ��Ʈ��ũ ��� �ֱ⸶�� ȣ��
    // ��Ʈ��ũ���� ó���� �۾�
    public override void FixedUpdateNetwork()
    {
        // �� �������� ��Ʈ��ũ ������Ʈ�� �ƴ� ���
        if (HasStateAuthority == false)
            return;
        transform.position += move * curMoveSpeed * Time.deltaTime;

        Rotates();
    }

    // ��Ʈ��ũ ������Ʈ�� �������� �� ȣ���
    //public override void Spawned()
    //{
    //    if (HasStateAuthority == true)
    //    {

    //    }
    //}

    private void Rotates()
    {
        // �÷��̾� ĳ������ ���� ȸ��
        transform.Rotate(Vector3.up * mouseX);

        // ī�޶��� ���� ȸ��
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
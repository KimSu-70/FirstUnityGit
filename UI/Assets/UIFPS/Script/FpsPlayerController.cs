using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FpsPlayerController : MonoBehaviour
{
    [SerializeField] private Transform playerCamera;
    [SerializeField] private float lookSpeed;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float sensitivity = 2.0f; // ���콺 ����
    [SerializeField] float repeatTime; // ���� �ӵ�

    Coroutine getbullet;

    private float verticalRotation = 0.0f; // ī�޶��� ���� ȸ�� ����


    [Header("UI")]
    [SerializeField] TextMeshProUGUI bulletText;
    [SerializeField] Slider bulletSlider;

    [Header("Model")]
    [SerializeField] FpsPlayerModel model;

    private void OnEnable()
    {
        model.OnbtChanged += Updatebullet;
    }

    private void OnDisable()
    {
        model.OnbtChanged -= Updatebullet;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Updatebullet(model.curBullet);
        bulletSlider.maxValue = model.MaxBullet;
    }

    private void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        transform.position += move * moveSpeed * Time.deltaTime;
    }

    private void Rotates()
    {
        // ���콺 �Է� �޾ƿ���
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // �÷��̾� ĳ������ ���� ȸ��
        transform.Rotate(Vector3.up * mouseX);

        // ī�޶��� ���� ȸ��
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -80f, 80f);
        playerCamera.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
    }

    IEnumerator GetBullet()
    {
        while (true)
        {
            Fire();
            yield return new WaitForSeconds(repeatTime);
        }
    }

    private void Fire()
    {
        if (model.curBullet > 0)
        {
            model.curBullet -= 1;
        }
    }

    #region UI

    private void Updatebullet(int bullet)
    {
        bulletText.text = $"{bullet} / {model.MaxBullet}";
        bulletSlider.value = bullet;
    }

    #endregion


    private void Update()
    {
        Rotates();
        Move();
        if(Input.GetMouseButtonDown(0))
        {
            getbullet = StartCoroutine(GetBullet());
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopCoroutine(getbullet);
        }

    }
}

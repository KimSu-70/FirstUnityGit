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
    [SerializeField] private float sensitivity = 2.0f; // 마우스 감도
    [SerializeField] float repeatTime; // 연사 속도

    Coroutine getbullet;

    private float verticalRotation = 0.0f; // 카메라의 수직 회전 각도


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
        // 마우스 입력 받아오기
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // 플레이어 캐릭터의 수평 회전
        transform.Rotate(Vector3.up * mouseX);

        // 카메라의 수직 회전
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

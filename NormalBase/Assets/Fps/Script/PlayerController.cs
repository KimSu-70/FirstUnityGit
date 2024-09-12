using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

public class PlayerController : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] PlayerModel model;
    [SerializeField] Animator player;
    [SerializeField] Transform camTransform;

    [Header("Fire")]
    [SerializeField] Transform pos;
    [SerializeField] TrailRenderer bullet;
    [SerializeField] AudioClips gun;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject HitEffect;
    [SerializeField] LayerMask layerMask;
    [SerializeField] float maxDistance;
    [SerializeField] float repeatTime;
    Coroutine fire;

    [Header("Fresh")]
    [SerializeField] GameObject fresh;
    private bool lsd;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        lsd = false;

        bullet.gameObject.SetActive(false);
    }

    private void Update()
    {
        Move();
        Fresh();

        if (Input.GetMouseButtonDown(0))
        {
            fire = StartCoroutine(Fires());
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopCoroutine(fire);
            bullet.gameObject.SetActive(false);
        }
    }

    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 camForward = camTransform.forward;
        Vector3 camRight = camTransform.right;

        camForward.y = 0;
        camRight.y = 0;

        Vector3 moveDirection = (camForward * z + camRight * x).normalized;

        if (moveDirection != Vector3.zero)
        {
            transform.Translate(moveDirection * model.MoveSpeed * Time.deltaTime, Space.World);
        }


        player.SetFloat("moveSpeed", z * model.MoveSpeed);
    }

    IEnumerator Fires()
    {
        while (true)
        {
            Fire();
            yield return new WaitForSeconds(repeatTime);
        }
    }

    private void Fire()
    {
        if (Physics.Raycast(pos.position, pos.forward, out RaycastHit hit, maxDistance, layerMask))
        {
            muzzleFlash.Play();
            gun.Gun();
            Instantiate(HitEffect, hit.point, Quaternion.LookRotation(hit.normal));
            
            bullet.gameObject.SetActive(true);
            bullet.Clear();
            bullet.AddPosition(pos.position);
            bullet.AddPosition(hit.point);
        }
    }

    private void Fresh()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            if(lsd == false)
            {
                fresh.SetActive(true);
                lsd = true;
            }
            else
            {
                fresh.SetActive(false);
                lsd = false;
            }
        }
    }
}

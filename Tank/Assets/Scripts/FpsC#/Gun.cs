using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class Gun : MonoBehaviour
{
    [SerializeField] Transform pos;
    [SerializeField] float repeatTime;

    private Coroutine gun;
    private Coroutine reload;

    private bool isReloading = false;

    [SerializeField] float maxDistance;
    [SerializeField] LayerMask layerMask;
    [SerializeField] int akt;
    [SerializeField] private int cout;
    private int couts;

    private void Start()
    {
        couts = cout;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isReloading)
        {
            gun = StartCoroutine(GunFire());
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopCoroutine(gun);
        }

        if (Input.GetKeyDown(KeyCode.R) && !isReloading)
        {
            reload = StartCoroutine(Reload());
        }
    }

    IEnumerator GunFire()
    {
        while (true)
        {
            Fire();
            yield return new WaitForSeconds(repeatTime);
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("장전중입니다.");
        yield return new WaitForSeconds(2f);
        cout = couts;
        Debug.Log("장전 완료!");
        isReloading = false ;
    }

    private void Fire()
    {
        if (cout > 0)
        {
            if (Physics.Raycast(pos.position, pos.forward, out RaycastHit hit, maxDistance, layerMask))
            {
                GameObject fps = hit.collider.gameObject;
                FpsMonster monster = fps.GetComponent<FpsMonster>();

                if (monster != null)
                {
                    monster.TakeDamage(akt);
                    cout--;
                    Debug.Log($"{cout}");
                }
            }
        }
        else if (cout <= 0)
        {
            Debug.Log("R을 눌러 장전해주세요.");
        }
    }
}

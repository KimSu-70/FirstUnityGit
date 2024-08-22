using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    public float jumpPower;

    public Rigidbody rigid;

    private void Awake()
    {
        Debug.Log("Awake 오브젝트 초기화!");
    }


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start 부족한 부분 마저 초기화 작업!");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            Debug.Log("Update 점프 정상 동작 확인!");
        }
    }

    private void OnEnable()
    {
        Debug.Log("OnEnable 활동시작!");
    }

    private void OnDisable()
    {
        Debug.Log("OnDisable 활동 끝!");
    }

    private void OnDestroy()
    {
        Debug.Log("OnDestroy 역할 마치고 소실!");
    }
}

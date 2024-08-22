using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    public float jumpPower;

    public Rigidbody rigid;

    private void Awake()
    {
        Debug.Log("Awake ������Ʈ �ʱ�ȭ!");
    }


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start ������ �κ� ���� �ʱ�ȭ �۾�!");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            Debug.Log("Update ���� ���� ���� Ȯ��!");
        }
    }

    private void OnEnable()
    {
        Debug.Log("OnEnable Ȱ������!");
    }

    private void OnDisable()
    {
        Debug.Log("OnDisable Ȱ�� ��!");
    }

    private void OnDestroy()
    {
        Debug.Log("OnDestroy ���� ��ġ�� �ҽ�!");
    }
}

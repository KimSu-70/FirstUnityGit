using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hits : MonoBehaviour
{
    [SerializeField] private int Level; // �༺�� ���� �ܰ�
    [SerializeField] private float sizeIncrement; // �ܰ躰 ũ�� ������

    private Transform planetTransform; // �༺�� Transform ������Ʈ
    private SpriteRenderer spriteRenderer; // �༺�� SpriteRenderer ������Ʈ

    private void Start()
    {
        // Transform�� SpriteRenderer ������Ʈ�� ĳ��
        planetTransform = transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdatePlanetSize(); // ũ�� ����
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �浹�� ��ü���� Hits ������Ʈ�� ������
        Hits Updates = collision.gameObject.GetComponent<Hits>();

        // �浹�� �༺�� ���� ������ ���� �ռ� ó��
        if (Updates && Updates.Level == Level)
        {
            MergePlanets(Updates); // �༺ �ռ�
        }
    }

    private void MergePlanets(Hits Updates)
    {
        Destroy(Updates.gameObject); // �浹�� �༺ ����
        Level++; // ���� ����
        UpdatePlanetSize(); // ũ�� ������Ʈ

        if (Level >= 3)
        {
            Debug.Log("���� ���� Ȯ��");
        }
    }

    private void UpdatePlanetSize()
    {
        if (Level != 1)
        {
            // ������ ���� �༺ ũ�� ����
            float newSize = planetTransform.localScale.x + sizeIncrement;
            planetTransform.localScale = new Vector3(newSize, newSize, 1f);
        }
        else
        {
            // �⺻ ũ�� ����
            planetTransform.transform.localScale = new Vector3(0.4f, 0.4f, 1f);
        }
    }
}

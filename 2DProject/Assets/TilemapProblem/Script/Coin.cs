using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // �÷��̾�� �浹���� ��
        {
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                player.CollectCoin(); // �÷��̾��� ���� ���� �޼��� ȣ��
                Destroy(gameObject); // ���� ������Ʈ ����
            }
        }
    }
}

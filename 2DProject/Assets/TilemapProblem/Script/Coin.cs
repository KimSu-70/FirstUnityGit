using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // 플레이어와 충돌했을 때
        {
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                player.CollectCoin(); // 플레이어의 코인 수집 메서드 호출
                Destroy(gameObject); // 코인 오브젝트 제거
            }
        }
    }
}

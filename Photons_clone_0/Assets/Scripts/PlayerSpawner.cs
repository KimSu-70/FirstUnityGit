using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class PlayerSpawner : SimulationBehaviour, IPlayerJoined, IPlayerLeft
{
    // IPlayerJoined 플레이어 입장 했을 때
    // IPlayerLeft 플레이어 나갔을 때

    [SerializeField] GameObject playerPrefab;
    [Range(0, 10)]
    [SerializeField] float randomRange;
    public void PlayerJoined(PlayerRef player)
    {
        Debug.Log("새로운 플레이어 참가");
        if (player != Runner.LocalPlayer)
            return;

        // 플레이어 스폰
        Vector3 spawnPos = new Vector3(Random.Range(-randomRange, randomRange), 0, Random.Range(-randomRange, randomRange));
        // Instantiate(playerPrefab, spawnPos, Quaternion.identity);  // 혼자만 보이게 만들 때
        Runner.Spawn(playerPrefab, spawnPos, Quaternion.identity); // 네트워크에서 다같이 만들기
    }

    public void PlayerLeft(PlayerRef player)
    {
        Debug.Log("플레이어 나감");
    }
}

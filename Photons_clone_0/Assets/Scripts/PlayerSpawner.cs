using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class PlayerSpawner : SimulationBehaviour, IPlayerJoined, IPlayerLeft
{
    // IPlayerJoined �÷��̾� ���� ���� ��
    // IPlayerLeft �÷��̾� ������ ��

    [SerializeField] GameObject playerPrefab;
    [Range(0, 10)]
    [SerializeField] float randomRange;
    public void PlayerJoined(PlayerRef player)
    {
        Debug.Log("���ο� �÷��̾� ����");
        if (player != Runner.LocalPlayer)
            return;

        // �÷��̾� ����
        Vector3 spawnPos = new Vector3(Random.Range(-randomRange, randomRange), 0, Random.Range(-randomRange, randomRange));
        // Instantiate(playerPrefab, spawnPos, Quaternion.identity);  // ȥ�ڸ� ���̰� ���� ��
        Runner.Spawn(playerPrefab, spawnPos, Quaternion.identity); // ��Ʈ��ũ���� �ٰ��� �����
    }

    public void PlayerLeft(PlayerRef player)
    {
        Debug.Log("�÷��̾� ����");
    }
}

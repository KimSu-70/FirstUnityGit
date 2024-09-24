using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileChecker : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;

    private void Start()
    {
        OnDrawGizmos();
    }
    private void OnDrawGizmos()
    {
        if (tilemap == null) return;

        Vector3Int origin = tilemap.origin; // Ÿ�ϸ��� ������
        Vector3Int size = tilemap.size; // Ÿ�ϸ��� ũ��

        // Ÿ�ϸ��� ũ�⸸ŭ �ݺ��մϴ�.
        for (int x = origin.x; x < origin.x + size.x; x++)
        {
            for (int y = origin.y; y < origin.y + size.y; y++)
            {
                Vector2Int position = new Vector2Int(x, y);
                // ���� ������ Ÿ���� ������, �Ұ����� Ÿ���� �Ķ������� ǥ��
                Gizmos.color = IsWalkable(position) ? Color.red : Color.blue;   // ���ǽ��� true�̸� red �ƴϸ� blue
                Gizmos.DrawCube(new Vector3(x +1f, y +1f, 0), Vector3.one * 0.5f);
            }
        }
    }

    private bool IsWalkable(Vector2Int pos)
    {
        TileBase tile = tilemap.GetTile(new Vector3Int(pos.x, pos.y, 0));
        return tile != null;
    }
}

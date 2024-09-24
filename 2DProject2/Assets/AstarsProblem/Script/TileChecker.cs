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

        Vector3Int origin = tilemap.origin; // 타일맵의 기준점
        Vector3Int size = tilemap.size; // 타일맵의 크기

        // 타일맵의 크기만큼 반복합니다.
        for (int x = origin.x; x < origin.x + size.x; x++)
        {
            for (int y = origin.y; y < origin.y + size.y; y++)
            {
                Vector2Int position = new Vector2Int(x, y);
                // 진행 가능한 타일은 빨간색, 불가능한 타일은 파란색으로 표시
                Gizmos.color = IsWalkable(position) ? Color.red : Color.blue;   // 조건식이 true이면 red 아니면 blue
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

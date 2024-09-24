using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstarPattern : MonoBehaviour
{
    [SerializeField] Transform startPos;
    [SerializeField] Transform endPos;

    [SerializeField] List<Vector2Int> path;

    public const int CostStraight = 10; // 상하좌우에 대한 계산 값
    public const int CostDiagonal = 14; // 대각선에 대한 계산 값

    #region AstarNode
    public class AstarNode
    {
        public Vector2Int pos;
        public AstarNode parent;

        public int f;
        public int g;
        public int h;

        public AstarNode(Vector2Int pos, AstarNode parent, int g, int h)
        {
            this.pos = pos;
            this.parent = parent;
            this.f = g + h;
            this.g = g;
            this.h = h;
        }
    }
    #endregion

    static Vector2Int[] direction =
    {
        new Vector2Int( 0, +1), // 상
        new Vector2Int( 0, -1), // 하
        new Vector2Int(-1,  0), // 좌
        new Vector2Int(+1,  0), // 우
    };

    private void Start()
    {
        Vector2Int start = new Vector2Int((int)startPos.position.x, (int)startPos.position.y);
        Vector2Int end = new Vector2Int((int)endPos.position.x, (int)endPos.position.y);

        bool success = Astar(start, end, out path);
        if (success)
        {
            Debug.Log("경로 탐색 성공");
        }
        else
        {
            Debug.Log("경로 탐색 실패");
        }
    }

    private void Update()
    {
        // path.Count - 1 : 마지막 점은 그 다음 점이 없기 때문에, 이 점에 대해 선을 그릴 필요가 없어 - 1
        // 예를 들어 총 5개의 [0,1,2,3,4] 점이 있다면 0-1, 1-2, 2-3 ,3-4 이런식으로 총 4개
        for (int i = 0; i < path.Count - 1; i++)
        {
            Vector3 from = new Vector3(path[i].x, path[i].y, 0);
            Vector3 to = new Vector3(path[i + 1].x, path[i + 1].y, 0);
            Debug.DrawLine(from, to, Color.red);
        }
    }


    public static bool Astar(Vector2Int start, Vector2Int end, out List<Vector2Int> path)
    {
        List<AstarNode> openList = new List<AstarNode>(); // 탐색해야 하는 정점 후보들을 보관하기 위해
        Dictionary<Vector2Int, bool> closeSet = new Dictionary<Vector2Int, bool>(); // 탐색을 완료한 정점 보관
        path = new List<Vector2Int> ();

        openList.Add(new AstarNode(start, null, 0, Heuristic(start, end))); // 처음으로 탐색할 정점! 그래서 부모 노드가 없으며, g값은 0으로 시작

        while (openList.Count > 0)  // 탐색할 정점이 없을 때까지 반복
        {
            // 1. 다음으로 탐색할 정점 선택하기 (F가 가장 낮은, F가 같다면 H가 가장 낮은 선택)
            AstarNode nextNode = NextNode(openList);

            // 2. 선택한 정점을 탐색했다고 표시
            openList.Remove(nextNode);          // 다음으로 탐색할 정점을 후보들 중에서 제거
            closeSet.Add(nextNode.pos, true);   // 탐색을 완료한 정점들에 추가

            // 3. 다음으로 탐색할 정점이 도착지인 경우 (경로 탐색을 성공 => path 반환하면서 종료)
            if (nextNode.pos == end)
            {
                AstarNode current = nextNode;
                while (current != null)
                {
                    path.Add(current.pos);
                    current = current.parent;
                }
                path.Add(start);
                path.Reverse();
                return true;
            }

            // 4. 주변 정점들의 점수를 계산
            for (int i = 0; i < direction.Length; i++)      // 방향에 대한 반복
            {
                // 점수를 넣어줄 위치
                Vector2Int pos = nextNode.pos + direction[i];

                // 탐색하면 안되는 경우 제외
                // 4-1. 이미 탐색한 정점이면
                if (closeSet.ContainsKey(pos))
                    continue;

                // 4-2. 못가는 지형일 경우
                // tilemap.HasTile : 타일맵을 분석하거나,
                // Physics.Overlap : 충돌체 존재여부를 확인하거나,
                // Physics.Raycast : 중간에 장애물이 없거나)
                if (Physics2D.OverlapCircle(pos, 0.4f) != null)
                    continue;

                // 4-3. 점수 계산
                int g = nextNode.g + CostStraight;
                int h = Heuristic(pos, end);
                int f = g + h;

                // 4-4. 정점의 점수 갱신이 필요한 경우
                AstarNode findNode = FindNode(openList, pos);
                // 점수가 없었던 경우
                if (findNode == null)
                {
                    openList.Add(new AstarNode(pos, nextNode, g, h));
                }
                // f 가 더 작아지거나
                else if (findNode.f > f)
                {
                    findNode.f = f;
                    findNode.g = g;
                    findNode.h = h;
                    findNode.parent = nextNode;
                }
            }
        }

        path = null;
        return false;
    }
    // 휴리스틱 (Heuristic) : 최상의 경로를 추정하는 순위값, 휴리스틱에 의해 경로탐색 효율이 결정됨
    public static int Heuristic(Vector2Int start, Vector2Int end)
    {
        int xSize = Mathf.Abs(start.x - end.x);
        int ySize = Mathf.Abs(start.y - end.y);

        // 맨해튼 거리 : 직선을 통해 이동하는 거리
        // return xSize + ySize;

        // 유클리드 거리 : 대각선을 통해 이동하는 거리
        // return (int)Vector2Int.Distance(start, end);

        // 타일맵 거리: 직선과 대각선을 통해 이동하는 거리
        int straightCount = Mathf.Abs(xSize - ySize);                   // Mathf.Abs 음수일 경우 양수로 변환하고, 양수일 경우 그대로 반환
        int diagonalCount = Mathf.Max(xSize, ySize) - straightCount;    // Mathf.Max Mathf.Max(a, b)는 a와 b 중 더 큰 값을 반환
        return CostStraight * straightCount + CostDiagonal * diagonalCount;
    }

    public static AstarNode NextNode(List<AstarNode> openList)
    {
        // F가 가장 낮은, F가 같다면 H가 가장 낮은 선택
        int curF = int.MaxValue;
        int curH = int.MaxValue;
        AstarNode minNode = null;   // 선택된 노드를 저장하는 변수

        for (int i = 0; i < openList.Count; i++) // 열린 리스트의 모든 노드 순회
        {
            // F(G + H) : 총 이동 거리
            // H : 예상 거리
            // F 값이 더 낮은 노드를 발견했을 때
            if (curF > openList[i].f)       // 현재 노드의 F 값이 curF보다 작으면
            {
                // curF와 curH를 현재 노드의 F와 H 값으로 업데이트
                curF = openList[i].f;       
                curH = openList[i].h;

                // 선택된 노드(minNode)를 현재 노드로 업데이트
                minNode = openList[i];
            }
            // F 값이 같고, H 값이 더 낮은 노드를 발견했을 때
            else if (curF == openList[i].f &&   // 현재 노드의 F 값이 curF와 같고
                curH > openList[i].h)           // 현재 H 값이 현재 노드의 H 값보다 크면
            {
                // 예상 거리(H)가 더 낮은 값으로 노드 값 선정
                curF = openList[i].f;
                curH = openList[i].h;
                minNode = openList[i];
            }
        }

        return minNode;
    }

    public static AstarNode FindNode(List<AstarNode> openList, Vector2Int pos)
    {
        for (int i = 0; i < openList.Count; i++)
        {
            if (openList[i].pos == pos)
            {
                return openList[i];
            }
        }

        return null;
    }
}

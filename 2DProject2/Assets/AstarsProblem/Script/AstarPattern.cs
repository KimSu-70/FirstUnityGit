using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstarPattern : MonoBehaviour
{
    [SerializeField] Transform startPos;
    [SerializeField] Transform endPos;

    [SerializeField] List<Vector2Int> path;

    public const int CostStraight = 10; // �����¿쿡 ���� ��� ��
    public const int CostDiagonal = 14; // �밢���� ���� ��� ��

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
        new Vector2Int( 0, +1), // ��
        new Vector2Int( 0, -1), // ��
        new Vector2Int(-1,  0), // ��
        new Vector2Int(+1,  0), // ��
    };

    private void Start()
    {
        Vector2Int start = new Vector2Int((int)startPos.position.x, (int)startPos.position.y);
        Vector2Int end = new Vector2Int((int)endPos.position.x, (int)endPos.position.y);

        bool success = Astar(start, end, out path);
        if (success)
        {
            Debug.Log("��� Ž�� ����");
        }
        else
        {
            Debug.Log("��� Ž�� ����");
        }
    }

    private void Update()
    {
        // path.Count - 1 : ������ ���� �� ���� ���� ���� ������, �� ���� ���� ���� �׸� �ʿ䰡 ���� - 1
        // ���� ��� �� 5���� [0,1,2,3,4] ���� �ִٸ� 0-1, 1-2, 2-3 ,3-4 �̷������� �� 4��
        for (int i = 0; i < path.Count - 1; i++)
        {
            Vector3 from = new Vector3(path[i].x, path[i].y, 0);
            Vector3 to = new Vector3(path[i + 1].x, path[i + 1].y, 0);
            Debug.DrawLine(from, to, Color.red);
        }
    }


    public static bool Astar(Vector2Int start, Vector2Int end, out List<Vector2Int> path)
    {
        List<AstarNode> openList = new List<AstarNode>(); // Ž���ؾ� �ϴ� ���� �ĺ����� �����ϱ� ����
        Dictionary<Vector2Int, bool> closeSet = new Dictionary<Vector2Int, bool>(); // Ž���� �Ϸ��� ���� ����
        path = new List<Vector2Int> ();

        openList.Add(new AstarNode(start, null, 0, Heuristic(start, end))); // ó������ Ž���� ����! �׷��� �θ� ��尡 ������, g���� 0���� ����

        while (openList.Count > 0)  // Ž���� ������ ���� ������ �ݺ�
        {
            // 1. �������� Ž���� ���� �����ϱ� (F�� ���� ����, F�� ���ٸ� H�� ���� ���� ����)
            AstarNode nextNode = NextNode(openList);

            // 2. ������ ������ Ž���ߴٰ� ǥ��
            openList.Remove(nextNode);          // �������� Ž���� ������ �ĺ��� �߿��� ����
            closeSet.Add(nextNode.pos, true);   // Ž���� �Ϸ��� �����鿡 �߰�

            // 3. �������� Ž���� ������ �������� ��� (��� Ž���� ���� => path ��ȯ�ϸ鼭 ����)
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

            // 4. �ֺ� �������� ������ ���
            for (int i = 0; i < direction.Length; i++)      // ���⿡ ���� �ݺ�
            {
                // ������ �־��� ��ġ
                Vector2Int pos = nextNode.pos + direction[i];

                // Ž���ϸ� �ȵǴ� ��� ����
                // 4-1. �̹� Ž���� �����̸�
                if (closeSet.ContainsKey(pos))
                    continue;

                // 4-2. ������ ������ ���
                // tilemap.HasTile : Ÿ�ϸ��� �м��ϰų�,
                // Physics.Overlap : �浹ü ���翩�θ� Ȯ���ϰų�,
                // Physics.Raycast : �߰��� ��ֹ��� ���ų�)
                if (Physics2D.OverlapCircle(pos, 0.4f) != null)
                    continue;

                // 4-3. ���� ���
                int g = nextNode.g + CostStraight;
                int h = Heuristic(pos, end);
                int f = g + h;

                // 4-4. ������ ���� ������ �ʿ��� ���
                AstarNode findNode = FindNode(openList, pos);
                // ������ ������ ���
                if (findNode == null)
                {
                    openList.Add(new AstarNode(pos, nextNode, g, h));
                }
                // f �� �� �۾����ų�
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
    // �޸���ƽ (Heuristic) : �ֻ��� ��θ� �����ϴ� ������, �޸���ƽ�� ���� ���Ž�� ȿ���� ������
    public static int Heuristic(Vector2Int start, Vector2Int end)
    {
        int xSize = Mathf.Abs(start.x - end.x);
        int ySize = Mathf.Abs(start.y - end.y);

        // ����ư �Ÿ� : ������ ���� �̵��ϴ� �Ÿ�
        // return xSize + ySize;

        // ��Ŭ���� �Ÿ� : �밢���� ���� �̵��ϴ� �Ÿ�
        // return (int)Vector2Int.Distance(start, end);

        // Ÿ�ϸ� �Ÿ�: ������ �밢���� ���� �̵��ϴ� �Ÿ�
        int straightCount = Mathf.Abs(xSize - ySize);                   // Mathf.Abs ������ ��� ����� ��ȯ�ϰ�, ����� ��� �״�� ��ȯ
        int diagonalCount = Mathf.Max(xSize, ySize) - straightCount;    // Mathf.Max Mathf.Max(a, b)�� a�� b �� �� ū ���� ��ȯ
        return CostStraight * straightCount + CostDiagonal * diagonalCount;
    }

    public static AstarNode NextNode(List<AstarNode> openList)
    {
        // F�� ���� ����, F�� ���ٸ� H�� ���� ���� ����
        int curF = int.MaxValue;
        int curH = int.MaxValue;
        AstarNode minNode = null;   // ���õ� ��带 �����ϴ� ����

        for (int i = 0; i < openList.Count; i++) // ���� ����Ʈ�� ��� ��� ��ȸ
        {
            // F(G + H) : �� �̵� �Ÿ�
            // H : ���� �Ÿ�
            // F ���� �� ���� ��带 �߰����� ��
            if (curF > openList[i].f)       // ���� ����� F ���� curF���� ������
            {
                // curF�� curH�� ���� ����� F�� H ������ ������Ʈ
                curF = openList[i].f;       
                curH = openList[i].h;

                // ���õ� ���(minNode)�� ���� ���� ������Ʈ
                minNode = openList[i];
            }
            // F ���� ����, H ���� �� ���� ��带 �߰����� ��
            else if (curF == openList[i].f &&   // ���� ����� F ���� curF�� ����
                curH > openList[i].h)           // ���� H ���� ���� ����� H ������ ũ��
            {
                // ���� �Ÿ�(H)�� �� ���� ������ ��� �� ����
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

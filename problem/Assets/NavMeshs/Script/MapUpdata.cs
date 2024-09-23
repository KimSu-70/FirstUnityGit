using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MapUpdata : MonoBehaviour
{
    public GameObject wallPrefab; // �� ������
    public Vector2 wallSize = new Vector2(1, 1); // ���� ũ��
    //[SerializeField] 

    private int[,] map =
    {
        {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
        {1,0,0,0,1,0,0,0,0,1,1,1,0,0,1},
        {1,0,0,0,1,0,0,0,0,0,1,1,0,0,1},
        {1,0,0,0,1,1,1,1,0,0,0,1,0,0,1},
        {1,0,0,1,1,0,0,1,0,0,0,0,0,0,1},
        {1,0,0,1,1,0,0,1,1,1,1,1,0,0,1},
        {1,0,0,1,1,0,0,0,0,0,1,1,0,0,1},
        {1,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
        {1,0,0,0,0,0,0,1,1,0,0,0,0,0,1},
        {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
    };

    private void Start()
    {
        GenerateMap();
        
    }

    private void GenerateMap()
    {
        int rows = map.GetLength(0);
        int cols = map.GetLength(1);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (map[i, j] != 0)
                {
                    Vector3 position = new Vector3(i * wallSize.x - 4.5f, -1.5f, j * wallSize.y - 4.5f);
                    GameObject wall = Instantiate(wallPrefab, position, Quaternion.identity);

                    // ���� ���� ��ũ��Ʈ�� �پ� �ִ� ��ü�� �ڽ����� �����մϴ�.
                    wall.transform.SetParent(transform);

                    Animator spawnStarter = wall.GetComponent<Animator>();
                    if (spawnStarter != null)
                    {
                        spawnStarter.SetTrigger("App");
                    }
                }
            }
        }
    }
}
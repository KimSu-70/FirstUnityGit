using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MapUpdata : MonoBehaviour
{
    public GameObject wallPrefab; // 벽 프리팹
    public Vector2 wallSize = new Vector2(1, 1); // 벽의 크기
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

                    // 벽을 현재 스크립트가 붙어 있는 객체의 자식으로 설정합니다.
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
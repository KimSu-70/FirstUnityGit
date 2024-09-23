using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CSVParser : MonoBehaviour
{
    public GameObject wallPrefab;
    public GameObject waterPrefab;
    public GameObject playerPrefab;
    public GameObject coinPrefab;

    private int mapWidth;
    private int mapHeight;

    private float wallHeight;

    void Start()
    {
        wallHeight = wallPrefab.transform.localScale.y;

        LoadMap("Assets/CsvJson/CSV/map.csv");
    }

    void LoadMap(string filePath)
    {
        List<string[]> csvData = new List<string[]>();

        using (StreamReader reader = new StreamReader(filePath))
        {
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                csvData.Add(line.Split(','));
            }
        }

        mapHeight = csvData.Count - 1;
        mapWidth = csvData[0].Length - 1;

        for (int y = 1; y <= mapHeight; y++)
        {
            for (int x = 1; x <= mapWidth; x++)
            {
                string tile = csvData[y][x].Trim();

                Vector3 position = new Vector3(x, -1f, -y);

                if (tile == "wall")
                {
                    GameObject wall = Instantiate(wallPrefab, position, Quaternion.identity);

                    wall.transform.SetParent(transform);


                    Animator spawnStarter = wall.GetComponent<Animator>();
                    if (spawnStarter != null)
                    {
                        spawnStarter.SetTrigger("App");
                    }
                }
                else if (tile == "water")
                {
                    GameObject water = Instantiate(waterPrefab, new Vector3(x, 0.02f, -y), Quaternion.identity);

                    water.transform.SetParent(transform);
                }
                else if (tile == "player")
                {
                    GameObject player = Instantiate(playerPrefab, new Vector3(x, 1f, -y), Quaternion.identity);

                    player.transform.SetParent(transform);
                }
                else if (tile == "coin")
                {
                    GameObject coin = Instantiate(coinPrefab, new Vector3(x, 0.5f, -y), Quaternion.identity);

                    coin.transform.SetParent(transform);
                }
            }
        }
    }
}
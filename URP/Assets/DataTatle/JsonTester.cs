using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]           // 인스펙터창에 띄우기
public class GameDate
{
    public string name;
    public int level;
    public float rate;
}

public class JsonTester : MonoBehaviour
{
    [SerializeField] GameDate gameDate;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            Save();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Load();
        }
    }

    [ContextMenu("Save")]
    private void Save()
    {
        string path = $"{Application.dataPath}/Save";

        if (Directory.Exists(path) == false)
        {
            Directory.CreateDirectory(path);
        }

        string json = JsonUtility.ToJson(gameDate);
        // 암호화 과정 추가
        File.WriteAllText($"{path}/save.txt", json);
    }

    [ContextMenu("Load")]
    private void Load()
    {
        string path = $"{Application.dataPath}/Save/save.txt";

        if(File.Exists(path) == false)
        {
            Debug.Log("불러올 세이브 파일이 없습니다");
            return;
        }

        string json = File.ReadAllText(path);
        // 복호화 과정 추가
        gameDate = JsonUtility.FromJson<GameDate>(json);
    }
}

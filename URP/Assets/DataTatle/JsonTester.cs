using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]           // �ν�����â�� ����
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
        // ��ȣȭ ���� �߰�
        File.WriteAllText($"{path}/save.txt", json);
    }

    [ContextMenu("Load")]
    private void Load()
    {
        string path = $"{Application.dataPath}/Save/save.txt";

        if(File.Exists(path) == false)
        {
            Debug.Log("�ҷ��� ���̺� ������ �����ϴ�");
            return;
        }

        string json = File.ReadAllText(path);
        // ��ȣȭ ���� �߰�
        gameDate = JsonUtility.FromJson<GameDate>(json);
    }
}

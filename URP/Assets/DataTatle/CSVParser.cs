using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public struct WeaponData
{
    public int index;
    public string name;
    public int attack;
    public int defense;
    public string description;
}

public class CSVParser : MonoBehaviour
{
    [SerializeField] List<WeaponData> weapons;
    public Dictionary<int, WeaponData> weaponsDatas = new Dictionary<int, WeaponData>();

    private void Awake()
    {
        // dataPath : 프로젝트 경로 => 게임 제작 중에 사용
        string path = Application.dataPath + "/DataTatle";

        if (Directory.Exists(path) == false)
        {
            Debug.Log("경로가 없습니다");
            return;
        }

        if (File.Exists(path + "/datatable.csv") == false)
        {
            Debug.Log("파일이 없습니다.");
            return;
        }


        string file = File.ReadAllText(path + "/datatable.csv");

        string[] lines = file.Split('\n');

        for (int i = 1; i < lines.Length; i++)
        {
            WeaponData weaponData = new WeaponData();

            string[] values = lines[i].Split(',', '\t');

            weaponData.index = int.Parse(values[0]);
            weaponData.name = values[1];
            weaponData.attack = int.Parse(values[2]);
            weaponData.defense = int.Parse(values[3]);
            weaponData.description = values[4];

            weapons.Add(weaponData);
            weaponsDatas.Add(weaponData.index, weaponData);
        }
    }
}

//public class Weapon : MonoBehaviour
//{
//    public int id;
//    public int attack => Manager.Data.WeaponData[id].attack;

//    private void Start()
//    {
        
//    }
//}
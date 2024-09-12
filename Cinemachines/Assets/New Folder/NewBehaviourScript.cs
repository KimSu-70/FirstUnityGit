using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public string name;
    void Start()
    {
        Task.Run(ThreadFunc);
    }

    void Update()
    {
        Debug.Log($"{name} 업데이트");
    }

    private void ThreadFunc()
    {
        for (int i = 0; i < 100; i++)
        {
            Debug.Log($"{name} 쓰레드 {i}");
        }
    }
}

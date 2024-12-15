using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    [SerializeField] public ReactiveProperty<int> Score;
    //public ReactiveProperty<Vector3>  = new ReactiveProperty<Vector3>;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Score.Value++;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerTester : MonoBehaviour
{
    [System.Flags]
    enum MonsyerType
    {
        Fire = 0001,        // = 1 << 0
        Water = 0010,       // = 1 << 1
        Grass = 0100,       // = 1 << 2
        Electric = 1000     // = 1 << 3
    }

    [SerializeField] MonsyerType monsyerType;

    [SerializeField] LayerMask layerMask;

    private void Start()
    {
        layerMask.Check(6);

        layerMask.UnCheck(5);

        // 1. 특정 레이어 체크 시키기
        // layerMask |= 1 << n;

        // layerMask : 0000 1110
        // 체크레이어 : 0100 0000
        // |  -----------------------
        // 결과      : 0010 1100

        // 1. 특정 레이어 해제 시키기
        // layerMask &= ~(1 << n);

        // layerMask : 0110 1100
        // 체크레이어 : 1011 1111
        // |  -----------------------
        // 결과      : 0010 1100

        // 1. 특정 레이어 체크 시키기
        // (layerMask & (1 << n)) != 0;

        // layerMask : 0110 1100
        // 체크레이어 : 0100 0000
        // |  -----------------------
        // 결과      : 0100 0000
    }
}

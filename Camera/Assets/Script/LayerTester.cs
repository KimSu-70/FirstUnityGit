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

        // 1. Ư�� ���̾� üũ ��Ű��
        // layerMask |= 1 << n;

        // layerMask : 0000 1110
        // üũ���̾� : 0100 0000
        // |  -----------------------
        // ���      : 0010 1100

        // 1. Ư�� ���̾� ���� ��Ű��
        // layerMask &= ~(1 << n);

        // layerMask : 0110 1100
        // üũ���̾� : 1011 1111
        // |  -----------------------
        // ���      : 0010 1100

        // 1. Ư�� ���̾� üũ ��Ű��
        // (layerMask & (1 << n)) != 0;

        // layerMask : 0110 1100
        // üũ���̾� : 0100 0000
        // |  -----------------------
        // ���      : 0100 0000
    }
}

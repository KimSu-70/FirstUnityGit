using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extension
{
    public static void Check(this ref LayerMask layerMask, int layer)
    {
        layerMask |= 1 << layer;
    }

    public static void UnCheck(this ref LayerMask layerMask, int layer)
    {
        layerMask &= ~(1 << layer);
    }

    public static bool Contain(this ref LayerMask layerMask, int layer)
    {
        return (layerMask & (1 << layer)) != 0;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FpsPlayerModel : MonoBehaviour
{
    [SerializeField] private int bullet;

    public int curBullet { get { return bullet; } set { bullet = value; OnbtChanged?.Invoke(bullet); } }
    public UnityAction<int> OnbtChanged;

    [SerializeField] int maxBullet;
    public int MaxBullet { get { return maxBullet; } }
}

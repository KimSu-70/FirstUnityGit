using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] TMP_Text textUI;

    public void SetScore(int score)
    {
        Debug.Log("�ٲ���.");
        textUI.text = score.ToString();
    }
}

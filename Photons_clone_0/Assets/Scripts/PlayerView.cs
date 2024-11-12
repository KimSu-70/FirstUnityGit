using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] PlayerModel model;
    [SerializeField] TMP_Text hpText;

    private void LateUpdate()
    {
        // transform.position = Camera.main.transform.forward;
    }

    private void Start()
    {
        hpText.text = model.hp.ToString();
    }

    private void OnEnable()
    {
        model.OnChangedHpEvent += SetHpText;
    }

    private void OnDisable()
    {
        model.OnChangedHpEvent -= SetHpText;
    }

    public void SetHpText(int hp)
    {
        hpText.text = hp.ToString();
    }
}

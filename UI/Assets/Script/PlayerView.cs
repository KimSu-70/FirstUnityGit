using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] PlayerModel model;
    private StringBuilder sb = new StringBuilder();

    private void OnEnable()
    {
        model.OnHPChanged += UpdateHP;
    }

    private void OnDisable()
    {
        model.OnHPChanged -= UpdateHP;
    }

    private void Start()
    {
        UpdateHP(model.HP);
    }

    private void UpdateHP(int hp)
    {
        sb.Clear();
        sb.Append(hp);
        hpText.SetText(sb);

        // hpText.text = $"{hp}";
    }
}

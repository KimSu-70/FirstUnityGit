using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPresenter : MonoBehaviour
{
    [SerializeField] PlayerModel model;
    [SerializeField] PlayerView view;

    [SerializeField] Button button;

    private void Start()
    {
        model.Score
            .Where(value => value >= 0)
            .Where(value => value <= 5)
            .Subscribe(value => view.SetScore(value))
            .AddTo(view);

        button.OnClickAsObservable()
            .Subscribe(value => model.Score.Value++);
    }
}

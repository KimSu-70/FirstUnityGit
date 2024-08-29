using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameState { Ready, Running, GameOver }

    [SerializeField] GameState curState;
    [SerializeField] PlayerController player;
    [SerializeField] TowerController[] towers;

    [Header("UI")]
    [SerializeField] GameObject readyText;
    [SerializeField] GameObject gameOverText;


    private void Start()
    {
        curState = GameState.Ready;

        // 게임씬에 있는 모든 컴포넌트 찾기
        // FindObjectsOfType
        // 단, 시간이 오래걸리는 함수이기 떄문에 로딩 과정에서 사용 권장
        towers = FindObjectsOfType<TowerController>();

        // 게임씬에 있는 특정 컴포넌트 찾기
        // 단, 시간이 올래걸리는 함수이기 때문에 로딩 과정에서 사용 권장
        player = FindObjectOfType<PlayerController>();
        player.OnDied += GameOver;

        readyText.SetActive(true);
        gameOverText.SetActive(false);
    }

    private void Update()
    {
        // anyKeyDown : 아무키나 눌렸을때
        if (curState == GameState.Ready && Input.anyKeyDown)
        {
            GameStart();
        }
        else if (curState == GameState.GameOver && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    public void GameStart()
    {
        curState = GameState.Running;
        // 타워들 공격개시
        foreach(TowerController tower in towers)
        {
            tower.StartAttack();
        }
        readyText.SetActive(false);
        gameOverText.SetActive(false);

    }

    public void GameOver()
    {
        curState = GameState.GameOver;
        // 타워들 공격중지
        foreach (TowerController tower in towers)
        {
            tower.StopAttack();
        }

        readyText.SetActive(false);
        gameOverText.SetActive(true);
    }
}

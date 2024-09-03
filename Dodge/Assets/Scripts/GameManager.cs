using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public enum GameState { Ready, Running, GameOver }

    [SerializeField] GameState curState;
    [SerializeField] PlayerController player;
    [SerializeField] TowerController[] towers;
    [SerializeField] GameObject clearZone;

    [Header("UI")]
    [SerializeField] GameObject readyText;
    [SerializeField] GameObject gameOverText;

    private float startTime;
    private float bestScore;
    private bool clearZoneActive;

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

        bestScore = PlayerPrefs.GetFloat("BestScore", 0f);
        clearZone.SetActive(false);
        clearZoneActive = false;
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
        if (curState == GameState.Running)
        {
            // 경과 시간 계산
            float elapsedTime = Time.time - startTime;

            // 20초 후 Clear Zone 활성화
            if (!clearZoneActive && elapsedTime >= 20f)
            {
                clearZone.SetActive(true);
                clearZoneActive = true;
            }
        }
    }

    public void GameStart()
    {
        curState = GameState.Running;
        startTime = Time.time;
        clearZone.SetActive(false);
        clearZoneActive = false;
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
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger detected with: " + other.gameObject.name);

        if (curState == GameState.Running && other.gameObject == player.gameObject)
        {
            float elapsedTime = Time.time - startTime;

            if (elapsedTime > bestScore)
            {
                bestScore = elapsedTime;
                PlayerPrefs.SetFloat("BestScore", bestScore);
            }

            GameOver();
        }
    }
}

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

        // ���Ӿ��� �ִ� ��� ������Ʈ ã��
        // FindObjectsOfType
        // ��, �ð��� �����ɸ��� �Լ��̱� ������ �ε� �������� ��� ����
        towers = FindObjectsOfType<TowerController>();

        // ���Ӿ��� �ִ� Ư�� ������Ʈ ã��
        // ��, �ð��� �÷��ɸ��� �Լ��̱� ������ �ε� �������� ��� ����
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
        // anyKeyDown : �ƹ�Ű�� ��������
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
            // ��� �ð� ���
            float elapsedTime = Time.time - startTime;

            // 20�� �� Clear Zone Ȱ��ȭ
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
        // Ÿ���� ���ݰ���
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
        // Ÿ���� ��������
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

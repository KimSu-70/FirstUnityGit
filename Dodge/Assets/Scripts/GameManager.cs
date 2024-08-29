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
    }

    public void GameStart()
    {
        curState = GameState.Running;
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
}

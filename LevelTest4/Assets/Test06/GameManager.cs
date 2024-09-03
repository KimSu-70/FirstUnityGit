using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameState { Ready, Running, GameOver }

    [SerializeField] GameState curState;
    [SerializeField] PlayerController player;
    [SerializeField] Monster[] monsters;

    [Header("UI")]
    [SerializeField] GameObject gameOverText;

    private float startTime;

    private void Start()
    {
        curState = GameState.Ready;

        monsters = FindObjectsOfType<Monster>();

        player = FindObjectOfType<PlayerController>();
        player.OnDied += GameOver;

        gameOverText.SetActive(false);
    }

    private void Update()
    {
        if (curState == GameState.GameOver && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("test06");
        }
    }

    public void GameOver()
    {
        curState = GameState.GameOver;

        gameOverText.SetActive(true);
    }
}

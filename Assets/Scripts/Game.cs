using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] private Screen _screen;
    [SerializeField] private Spawner _spawner;

    private void OnEnable()
    {
        _screen.StartButtonClick += OnStartButtonClicked;
        _screen.RestartButtonClick += OnExitButtonClicked;
        _spawner.OnFinish += OnGameOver;
    }

    private void OnDisable()
    {
        _screen.StartButtonClick -= OnStartButtonClicked;
        _screen.RestartButtonClick -= OnExitButtonClicked;
        _spawner.OnFinish -= OnGameOver;
    }


    private void Start()
    {
        Time.timeScale = 0;
        _screen.gameObject.SetActive(true);
        _screen.OpenStart();
    }

    private void OnStartButtonClicked()
    {
        _screen.gameObject.SetActive(false);
        StartGame();
    }
    private void OnExitButtonClicked()
    {
        SceneManager.LoadSceneAsync(0);
    }

    private void StartGame()
    {
        Time.timeScale = 1;
        _spawner.StartGame();
    }

    private void OnGameOver()
    {
        Time.timeScale = 0;
        _screen.gameObject.SetActive(true);
        _screen.OpenRestart();
    }
}

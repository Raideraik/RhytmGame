using NTC.Global.Cache;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoCache
{
    [SerializeField] private StartScreen _startScreen;
    [SerializeField] private GameOverScreen _gameoverScreen;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Transform _characterSpawnPoint;

    private LoadSkin _loadSkin;


    protected override void OnEnabled()
    {
        _startScreen.StartButtonClick += StartGame;
        _gameoverScreen.ExitButtonClick += ExitGame;
        _spawner.OnFinish += OnGameOver;
    }

    protected override void OnDisabled()
    {
        _startScreen.StartButtonClick -= StartGame;
        _gameoverScreen.ExitButtonClick -= ExitGame;
        _spawner.OnFinish -= OnGameOver;
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
        _loadSkin = Get<LoadSkin>();
        Instantiate(_loadSkin.GetChoosedSkin().GetPrefab(), _characterSpawnPoint);
        Time.timeScale = 0;

        _startScreen.gameObject.SetActive(true);
        _startScreen.OpenStart();
    }

    private void BeginGame()
    {
        _startScreen.gameObject.SetActive(false);
        StartGame();
        Instantiate(_loadSkin.GetChoosedSkin().GetPrefab(), _characterSpawnPoint);
    }
    private void ExitGame()
    {
        SceneManager.LoadSceneAsync(0);
    }

    private void StartGame()
    {
        _startScreen.gameObject.SetActive(false);
        Time.timeScale = 1;
        AudioFlow.Instance.StartFlow();
        _spawner.StartGame();
    }

    private void OnGameOver()
    {
        Time.timeScale = 0;
        _gameoverScreen.gameObject.SetActive(true);
        _gameoverScreen.OpenGameOverScreen();
    }
}

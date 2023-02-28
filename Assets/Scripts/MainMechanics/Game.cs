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
    [SerializeField] private float _additionalTimeForSpawn;
    private LoadSkin _loadSkin;


    protected override void OnEnabled()
    {
        _startScreen.StartButtonClick += OnStartButtonClick;
        _gameoverScreen.ExitButtonClick += ExitGame;
        _spawner.OnFinish += OnGameOver;
    }

    protected override void OnDisabled()
    {
        _startScreen.StartButtonClick -= OnStartButtonClick;
        _gameoverScreen.ExitButtonClick -= ExitGame;
        _spawner.OnFinish -= OnGameOver;
    }

    private void Awake()
    {
        Application.targetFrameRate = 60;
        _loadSkin = Get<LoadSkin>();
    }

    private void Start()
    {
        Time.timeScale = 0;

        _startScreen.gameObject.SetActive(true);
        _startScreen.OpenStart();
    }
    private void ExitGame()
    {
        SceneFader.Instance.FadeTo(0);
        // SceneManager.LoadSceneAsync(0);
    }

    private void OnStartButtonClick()
    {
        _startScreen.gameObject.SetActive(false);
        Time.timeScale = 1;
        StartCoroutine(StartGame());
    }

    private void OnGameOver()
    {
        Time.timeScale = 0;
        _gameoverScreen.gameObject.SetActive(true);
        _gameoverScreen.OpenGameOverScreen();
    }

    private IEnumerator StartGame()
    {
        Instantiate(_loadSkin.GetChoosedSkin().GetSpawnEffect(), _characterSpawnPoint);
        yield return new WaitForSeconds(_loadSkin.GetChoosedSkin().GetSpawnEffect().GetFloat("Duration") + _additionalTimeForSpawn);
        Instantiate(_loadSkin.GetChoosedSkin().GetPrefab(), _characterSpawnPoint);


        AudioFlow.Instance.StartFlow();
        _spawner.StartGame();
    }
}

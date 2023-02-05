using NTC.Global.Cache;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoCache
{
    [SerializeField] private MenuScreen _screen;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private ObjectPlacer _objectPlacer;
    [SerializeField] private GameObject _character;

    protected override void OnEnabled()
    {
        _screen.StartButtonClick += OnStartButtonClicked;
        _screen.RestartButtonClick += OnExitButtonClicked;
        _screen.SpawnCharacterButtonClikck += OnSpawnCharacterButtonClicked;
        _spawner.OnFinish += OnGameOver;
    }

    protected override void OnDisabled()
    {
        _screen.StartButtonClick -= OnStartButtonClicked;
        _screen.RestartButtonClick -= OnExitButtonClicked;
        _screen.SpawnCharacterButtonClikck -= OnSpawnCharacterButtonClicked;
        _spawner.OnFinish -= OnGameOver;
    }

    private void Start()
    {
        Application.targetFrameRate = 120;
        Time.timeScale = 0;
        _screen.gameObject.SetActive(true);

        _screen.OpenStart();
    }

    private void OnSpawnCharacterButtonClicked()
    {
        _objectPlacer.InstalCharacter();
        StartGame();
        _screen.gameObject.SetActive(false);

    }

    private void OnStartButtonClicked()
    {
        _screen.gameObject.SetActive(false);
        StartGame();
        _character.SetActive(true);
    }
    private void OnExitButtonClicked()
    {
        SceneManager.LoadSceneAsync(0);
    }

    private void StartGame()
    {

        Time.timeScale = 1;
        AudioFlow.Instance.StartFlow();
        _spawner.StartGame();

    }

    private void OnGameOver()
    {
        Time.timeScale = 0;
        _screen.gameObject.SetActive(true);
        _screen.OpenRestart();
    }
}

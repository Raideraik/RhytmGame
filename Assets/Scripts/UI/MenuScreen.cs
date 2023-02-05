using NTC.Global.Cache;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MenuScreen : MonoCache
{
    public event UnityAction StartButtonClick;
    public event UnityAction RestartButtonClick;
    public event UnityAction SpawnCharacterButtonClikck;

    [SerializeField] private Button _startButton;
    [SerializeField] private Button _reStartButton;
    [SerializeField] private Button _spawnCharacterButton;
    protected override void OnEnabled()
    {
        _startButton.onClick.AddListener(StartGame);
        _reStartButton.onClick.AddListener(RestartGame);
        _spawnCharacterButton.onClick.AddListener(SpawnCharacker);
    }

    protected override void OnDisabled()
    {
        _startButton.onClick.RemoveListener(StartGame);
        _reStartButton.onClick.RemoveListener(RestartGame);
        _spawnCharacterButton.onClick.RemoveListener(SpawnCharacker);
    }

    private void StartGame()
    {
        StartButtonClick?.Invoke();
    }

    private void RestartGame()
    {
        RestartButtonClick?.Invoke();
    }

    private void SpawnCharacker()
    {
        SpawnCharacterButtonClikck?.Invoke();
    }

    public void OpenStart()
    {
        _startButton.gameObject.SetActive(true);
        _reStartButton.gameObject.SetActive(false);
        _spawnCharacterButton.gameObject.SetActive(true);
    }
    public void OpenRestart()
    {
        _startButton.gameObject.SetActive(false);
        _spawnCharacterButton.gameObject.SetActive(false);
        _reStartButton.gameObject.SetActive(true);
    }
}

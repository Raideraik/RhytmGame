using NTC.Global.Cache;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StartScreen : MonoCache
{
    public event UnityAction StartButtonClick;
    public event UnityAction SpawnCharacterButtonClikck;

    [SerializeField] private Button _startButton;
    [SerializeField] private Button _spawnCharacterButton;
    protected override void OnEnabled()
    {
        _startButton.onClick.AddListener(StartGame);
        _spawnCharacterButton.onClick.AddListener(SpawnCharacker);
    }

    protected override void OnDisabled()
    {
        _startButton.onClick.RemoveListener(StartGame);
        _spawnCharacterButton.onClick.RemoveListener(SpawnCharacker);
    }

    private void StartGame()
    {
        StartButtonClick?.Invoke();
        AudioEffectsControll.Instance.PlayButtonClip();
    }

    private void SpawnCharacker()
    {
        SpawnCharacterButtonClikck?.Invoke();
        AudioEffectsControll.Instance.PlayButtonClip();
    }

    public void OpenStart()
    {
        _startButton.gameObject.SetActive(true);
        _spawnCharacterButton.gameObject.SetActive(true);
    }
}

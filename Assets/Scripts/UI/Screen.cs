using NTC.Global.Cache;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Screen : MonoCache
{

    public event UnityAction StartButtonClick;
    public event UnityAction RestartButtonClick;

    [SerializeField] private Button _startButton;
    [SerializeField] private Button _reStartButton;
    protected override void OnEnabled()
    {
        _startButton.onClick.AddListener(StartGame);
        _reStartButton.onClick.AddListener(RestartGame);
    }

    protected override void OnDisabled()
    {
        _startButton.onClick.RemoveListener(StartGame);
        _reStartButton.onClick.RemoveListener(RestartGame);
    }

    private void StartGame()
    {
        StartButtonClick?.Invoke();
    }

    private void RestartGame()
    {
        RestartButtonClick?.Invoke();
    }

    public void OpenStart()
    {
        _startButton.gameObject.SetActive(true);
        _reStartButton.gameObject.SetActive(false);
    }
    public void OpenRestart()
    {
        _startButton.gameObject.SetActive(false);
        _reStartButton.gameObject.SetActive(true);
    }
}

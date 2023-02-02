using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Screen : MonoBehaviour
{

    public event UnityAction StartButtonClick;
    public event UnityAction RestartButtonClick;

    [SerializeField] private Button _startButton;
    [SerializeField] private Button _reStartButton;
    private void OnEnable()
    {
        _startButton.onClick.AddListener(StartGame);
        _reStartButton.onClick.AddListener(RestartGame);
    }

    private void OnDisable()
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

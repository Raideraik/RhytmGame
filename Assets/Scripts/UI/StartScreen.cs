using NTC.Global.Cache;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StartScreen : MonoCache
{
    public event UnityAction StartButtonClick;

    [SerializeField] private Button _startButton;
    protected override void OnEnabled()
    {
        _startButton.onClick.AddListener(StartGame);
        CharacterVisual._onEnded += OpenStart;
    }

    protected override void OnDisabled()
    {
        _startButton.onClick.RemoveListener(StartGame);
        CharacterVisual._onEnded -= OpenStart;
    }

    private void StartGame()
    {
        StartButtonClick?.Invoke();
        AudioEffectsControll.Instance.PlayButtonClip();
    }

    private void SpawnCharacker()
    {
        AudioEffectsControll.Instance.PlayButtonClip();
    }

    public void OpenStart()
    {
        _startButton.gameObject.SetActive(true);
    }
}

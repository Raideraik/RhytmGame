using NTC.Global.Cache;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoseScreen : MonoCache
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _returnButton;
    [SerializeField] private GameObject _losePanel;
    [SerializeField] private CoolnessLevel _coolnessLevel;

    protected override void OnEnabled()
    {
        _restartButton.onClick.AddListener(RestartLevel);
        _returnButton.onClick.AddListener(ReturnToMainMenu);
        _coolnessLevel.OnLoseGame += LoseGame;
    }

    protected override void OnDisabled()
    {
        _restartButton.onClick.RemoveListener(RestartLevel);
        _returnButton.onClick.RemoveListener(ReturnToMainMenu);
        _coolnessLevel.OnLoseGame -= LoseGame;
    }

    private void LoseGame()
    {
        Time.timeScale = 0;
        _losePanel.SetActive(true);
    }

    private void RestartLevel()
    {
        SceneFader.Instance.FadeTo(1);

        // SceneManager.LoadSceneAsync(1);
    }

    private void ReturnToMainMenu()
    {
        SceneFader.Instance.FadeTo(0);

        // SceneManager.LoadSceneAsync(0);
    }
}

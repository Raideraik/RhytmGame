using NTC.Global.Cache;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoCache
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private Button _pauseButtonClick;
    [SerializeField] private Button _continueButtonClick;
    [SerializeField] private Button _exitButtonClick;

    protected override void OnEnabled()
    {
        _pauseButtonClick.onClick.AddListener(PauseGame);
        _continueButtonClick.onClick.AddListener(ContinueGame);
        _exitButtonClick.onClick.AddListener(ReturnToMainMenu);
    }

    protected override void OnDisabled()
    {
        _pauseButtonClick.onClick.RemoveListener(PauseGame);
        _continueButtonClick.onClick.RemoveListener(ContinueGame);
        _exitButtonClick.onClick.RemoveListener(ReturnToMainMenu);
    }

    private void PauseGame()
    {
        _pauseMenu.SetActive(true);
        Time.timeScale = 0;
        AudioFlow.Instance.PauseFlow();
    }
    private void ContinueGame()
    {
        _pauseMenu.SetActive(false);
        Time.timeScale = 1;
        AudioFlow.Instance.ContinueFlow();
    }

    private void ReturnToMainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }
}

using NTC.Global.Cache;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoCache
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private Button _pauseButtonClick;
    [SerializeField] private Button _continueButtonClick;
    [SerializeField] private Button _exitButtonClick;
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private float _secondsBeforeContinue;
    [SerializeField] private LoseScreen _loseScreen;

    private bool _isGameStarted = false;
    protected override void OnEnabled()
    {
        _pauseButtonClick.onClick.AddListener(PauseGame);
        _continueButtonClick.onClick.AddListener(ContinueGame);
        _exitButtonClick.onClick.AddListener(ReturnToMainMenu);
        Game.OnGameStarted += GameStarted;
    }

    protected override void OnDisabled()
    {
        _pauseButtonClick.onClick.RemoveListener(PauseGame);
        _continueButtonClick.onClick.RemoveListener(ContinueGame);
        _exitButtonClick.onClick.RemoveListener(ReturnToMainMenu);
        Game.OnGameStarted -= GameStarted;
    }

    private void PauseGame()
    {
        AudioEffectsControll.Instance.PlayButtonClip();
        _pauseMenu.SetActive(true);
        Time.timeScale = 0;
        if (_isGameStarted)
            AudioFlow.Instance.PauseFlow();
    }
    private void ContinueGame()
    {
        AudioEffectsControll.Instance.PlayButtonClip();
        _pauseMenu.SetActive(false);
        StartCoroutine(Timer(_secondsBeforeContinue));
    }

    private void ReturnToMainMenu()
    {
        //AudioEffectsControll.Instance.PlayButtonClip();
        SceneFader.Instance.FadeTo(0);
        // SceneManager.LoadSceneAsync(0);
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause && _isGameStarted && !_loseScreen.IsLost)
        {
            PauseGame();
        }

    }

    private void GameStarted()
    {
        _isGameStarted = true;
    }

    private IEnumerator Timer(float seconds)
    {
        _timerText.gameObject.SetActive(true);

        float totalTime = 0;
        while (totalTime <= seconds)
        {
            seconds -= Time.unscaledDeltaTime;
            int numb = (int)seconds + 1;
            _timerText.text = numb.ToString();
            yield return null;
        }
        _timerText.gameObject.SetActive(false);
        Time.timeScale = 1;
        AudioFlow.Instance.ContinueFlow();
    }
}

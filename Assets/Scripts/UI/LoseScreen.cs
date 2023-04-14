using NTC.Global.Cache;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoseScreen : MonoCache
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _returnButton;
    [SerializeField] private GameObject _losePanel;
    [SerializeField] private CoolnessLevel _coolnessLevel;

    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private float _secondsBeforeContinue;
    private bool _isLost = false;
    public bool IsLost => _isLost;

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
        AudioFlow.Instance.PauseFlow();
        Time.timeScale = 0;
        _losePanel.SetActive(true);
        _isLost = true;
    }

    private void RestartLevel()
    {
        SceneFader.Instance.FadeTo(SceneManager.GetActiveScene().buildIndex);

        // SceneManager.LoadSceneAsync(1);
    }

    private void ReturnToMainMenu()
    {
        SceneFader.Instance.FadeTo(0);

        // SceneManager.LoadSceneAsync(0);
    }

    public void ContinueGame()
    {
        AudioEffectsControll.Instance.PlayButtonClip();
        _losePanel.SetActive(false);
        _isLost = false;
        _coolnessLevel.ResetCoolnessLevel();
        StartCoroutine(Timer(_secondsBeforeContinue));
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

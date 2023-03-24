using NTC.Global.Cache;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameOverScreen : MonoCache
{
    public event UnityAction ExitButtonClick;

    [SerializeField] private AdsSkippable _adsInterestial;

    [SerializeField] private Score _score;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Sprite _starEarned;
    [SerializeField] private TMP_Text _scoreText;

    [SerializeField] private Image[] _starsEarnedImage;
    [SerializeField] private GameObject[] _starEffects;
    [SerializeField] private GameObject _screen;
    [SerializeField] private float _showingTime;
    [SerializeField] private float _subdivider = 1;

    protected override void OnEnabled()
    {
        _exitButton.onClick.AddListener(ExitClick);
    }

    protected override void OnDisabled()
    {
        _exitButton.onClick.RemoveListener(ExitClick);
    }

    private void ExitClick()
    {
        ExitButtonClick?.Invoke();
        AudioEffectsControll.Instance.PlayButtonClip();
    }

    public void OpenGameOverScreen()
    {
        _screen.SetActive(true);
        UpdateScore();
        CountStars();
    }

    private void UpdateScore()
    {
        _scoreText.text = _score.GetScore().ToString();
    }

    private void CountStars()
    {
        float percentage = 0;
        int starsEarned;

        percentage += _score.GetScore() / _subdivider;
        percentage /= AudioFlow.Instance.GetSong().NeededScore;
        percentage *= 100;
        starsEarned = 0;

        if (percentage >= 90)
        {
            starsEarned = 4;
        }
        else if (percentage >= 70 && percentage < 90)
        {
            starsEarned = 3;
        }
        else if (percentage >= 50 && percentage < 70)
        {
            starsEarned = 2;
        }
        else if (percentage >= 30 && percentage < 50)
        {
            starsEarned = 1;
        }
        else if (percentage >= 0 && percentage < 30)
        {
            starsEarned = 0;
        }


        for (int i = 0; i < _starEffects.Length; i++)
        {
            if (i <= starsEarned)
            {
                _starEffects[i].SetActive(true);

                StartCoroutine(ShowStars(starsEarned, _showingTime));

            }
        }
    }

    private IEnumerator ShowStars(int earnedStars, float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);

        for (int i = 0; i < _starsEarnedImage.Length; i++)
        {
            if (i <= earnedStars)
                _starsEarnedImage[i].sprite = _starEarned;
        }

        StartCoroutine(ShowAds());
    }

    private IEnumerator ShowAds()
    {
        yield return new WaitForSecondsRealtime(1f);
        _adsInterestial.ShowAd();
    }

}

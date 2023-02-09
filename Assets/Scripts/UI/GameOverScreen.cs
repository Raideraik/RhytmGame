using NTC.Global.Cache;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameOverScreen : MonoCache
{
    public event UnityAction ExitButtonClick;

    [SerializeField] private Score _score;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Image _starsEarnedImage;

    [SerializeField] private Sprite[] _stars;
    [SerializeField] private GameObject _screen;

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
        CountStars();
    }

    private void CountStars()
    {
        float percentage = 0;
        int starsEarned;

        percentage += _score.GetScore();
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

        _starsEarnedImage.sprite = _stars[starsEarned];
    }

}

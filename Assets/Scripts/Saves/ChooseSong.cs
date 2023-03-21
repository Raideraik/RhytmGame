using NTC.Global.Cache;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseSong : MonoCache
{
    [SerializeField] private Button _button;
    [SerializeField] private Image[] _scoreStars;
    [SerializeField] private Sprite _starSprite;
    [SerializeField] private TMP_Text _songName;

    private Song _song;
    // private int _score;
    protected override void OnEnabled()
    {
        _button.onClick.AddListener(ChooseLevel);
    }

    protected override void OnDisabled()
    {
        _button.onClick.RemoveListener(ChooseLevel);
    }

    private void Start()
    {
        //_score = PlayerPrefs.GetInt(_song.SongName + "_Score", 0);
        //SetStars();
        if (IsCanChooseLevel())
        {
            _songName.text = _song.SongName;
        }
        else
        {
            _songName.text = "Need " + (_song.NeededStars - Stars.Instance.GetStars()) + " stars";
        }
    }

    private bool IsCanChooseLevel()
    {
        if (Stars.Instance.GetStars() >= _song.NeededStars)
        {
            return true;
        }

        return false;
    }

    private void ChooseLevel()
    {
        if (IsCanChooseLevel())
        {
            AudioEffectsControll.Instance.PlayButtonClip();
            PlayerPrefs.SetInt("ChoosedSong", _song.Id);
            SceneFader.Instance.FadeTo(1);

        }
        //SceneManager.LoadSceneAsync(1);
    }

    public void SetStars()
    {
        int stars = 0;

        float percentage = 0;
        percentage += MainMenuScore.Instance.GetSongScore(_song);
        percentage /= _song.NeededScore;
        percentage *= 100f;

        if (percentage >= 90)
        {
            stars = 5;
        }
        else if (percentage >= 70 && percentage < 90)
        {
            stars = 4;
        }
        else if (percentage >= 50 && percentage < 70)
        {
            stars = 3;
        }
        else if (percentage >= 30 && percentage < 50)
        {
            stars = 2;
        }
        else if (percentage > 0 && percentage < 30)
        {
            stars = 1;
        }


        if (stars != 0)
        {
            for (int i = 0; i < stars; i++)
            {
                _scoreStars[i].sprite = _starSprite;
            }
        }

        Stars.Instance.AddStars(stars);
        Stars.Instance.UpdateStars();
    }

    public void SetSong(Song song)
    {
        _song = song;
    }
}

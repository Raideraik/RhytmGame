using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class MainMenuScore : MonoBehaviour
{
    public static MainMenuScore Instance { get; private set; }
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private Song[] _songList;

    private int _score;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There more than one MainMenuScore!" + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {/*
        for (int i = 0; i < _songList.Length; i++)
        {
            _score += PlayerPrefs.GetInt(_songList[i].SongName + "_Score");
        }*/

        _score = PlayerPrefs.GetInt("AllScore");
        UpdateScore();
    }
    private void UpdateScore()
    {
        _scoreText.text = _score.ToString();
        //PlayerPrefs.SetInt("AllScore", _score);
    }

    public bool TrySell(int score)
    {
        if (_score >= score)
        {
            _score -= score;
            PlayerPrefs.SetInt("AllScore", _score);
            UpdateScore();
            return true;
        }
        else
        {
            return false;
        }
    }

    public int GetSongScore(Song song)
    {
        for (int i = 0; i < _songList.Length; i++)
        {
            if (_songList[i] == song)
            {
                return PlayerPrefs.GetInt(_songList[i].SongName + "_Score", 0);
            }
        }

        return 0;
    }

}

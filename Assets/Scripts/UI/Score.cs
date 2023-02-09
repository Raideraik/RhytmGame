using NTC.Global.Cache;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoCache
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _scoreMultiplierText;
    [SerializeField] private CollectorController[] _collector;

    [SerializeField] private int[] _scoreNeededToAddTimes;
    [SerializeField] private int[] _scoreMultiplier;

    [SerializeField] private Spawner _spawner;

    private int _index = 0;
    private int _score;
    private int _scoreAddedTimes;
    protected override void OnEnabled()
    {
        for (int i = 0; i < _collector.Length; i++)
        {
            _collector[i].OnCollected += AddScore;
            _collector[i].OnWrong += ResetMultiplier;
        }

        _spawner.OnFinishSave += SaveScore;
    }
    protected override void OnDisabled()
    {
        for (int i = 0; i < _collector.Length; i++)
        {
            _collector[i].OnCollected -= AddScore;
            _collector[i].OnWrong -= ResetMultiplier;
        }
        _spawner.OnFinishSave -= SaveScore;

    }
    private void SaveScore(string name)
    {
        if (PlayerPrefs.GetInt(name + "_Score", 0) < _score)
            PlayerPrefs.SetInt(name + "_Score", _score);

        Debug.Log(PlayerPrefs.GetInt(name + "_Score", 0));
    }
    private void AddScore()
    {
        _score += _scoreMultiplier[_index];
        _scoreText.text = _score.ToString();
        _scoreAddedTimes++;
        _scoreMultiplierText.text = _scoreMultiplier[_index].ToString();

        if (_scoreAddedTimes >= _scoreNeededToAddTimes[_index] && _index < _scoreNeededToAddTimes.Length - 1)
        {
            _index++;
        }
    }

    private void ResetMultiplier()
    {
        _scoreAddedTimes = 0;
        _index = 0;
        _scoreMultiplierText.text = _scoreMultiplier[_index].ToString();
    }

    public int GetScore()
    {
        return _score;
    }
}

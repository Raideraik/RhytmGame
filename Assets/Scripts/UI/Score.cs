using NTC.Global.Cache;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Score : MonoCache
{
    public event UnityAction OnMultiplierChanged;

    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _scoreMultiplierText;
    //[SerializeField] private CollectorController[] _collector;
    //[SerializeField] private ControllersHandler _controllersHandler;

    [SerializeField] private int[] _scoreNeededToAddTimes;
    [SerializeField] private int[] _scoreMultiplier;

    [SerializeField] private Spawner _spawner;

    private int _index = 0;
    private int _score;
    private int _scoreAddedTimes;

    private string _songName;
    protected override void OnEnabled()
    {

        ControllersHandler.OnAnyCollected += AddScore;
        ControllersHandler.OnAnyMissed += ResetMultiplier;


        _spawner.OnFinishSave += SaveScore;
    }
    protected override void OnDisabled()
    {

        ControllersHandler.OnAnyCollected -= AddScore;
        ControllersHandler.OnAnyMissed -= ResetMultiplier;

        _spawner.OnFinishSave -= SaveScore;

    }
    private void SaveScore(string name)
    {
        if (PlayerPrefs.GetInt(name + "_Score", 0) < _score)
            PlayerPrefs.SetInt(name + "_Score", _score);

        if (_songName == "")
            _songName = name;

        // Debug.Log(PlayerPrefs.GetInt(name + "_Score", 0));
    }

    public void RewardPlayer()
    {
        _score *= 2;

        SaveScore(_songName);
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
            OnMultiplierChanged?.Invoke();
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

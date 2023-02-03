using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _scoreMultiplierText;
    [SerializeField] private CollectorController[] _collector;

    [SerializeField] private int[] _scoreNeededToAddTimes;
    [SerializeField] private int[] _scoreMultiplier;
    [SerializeField] private string[] _animatonsNames;


    private int _index = 0;
    private int _score;
    private int _scoreAddedTimes;
    private void OnEnable()
    {
        for (int i = 0; i < _collector.Length; i++)
        {
            _collector[i].OnCollected += AddScore;
            _collector[i].OnWrong += ResetMultiplier;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < _collector.Length; i++)
        {
            _collector[i].OnCollected -= AddScore;
            _collector[i].OnWrong -= ResetMultiplier;
        }

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
            _animator.Play(_animatonsNames[_index]);
        }
    }

    private void ResetMultiplier()
    {
        _scoreAddedTimes = 0;
        _index = 0;
        _scoreMultiplierText.text = _scoreMultiplier[_index].ToString();
        _animator.Play("Idle");

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dan.Main;
using TMPro;

public class AuthrizationScreen : MonoBehaviour
{
    [SerializeField] private LeaderBoard _leaderBoard;
    [SerializeField] private TMP_InputField _inputName;
    [SerializeField] private TMP_Text _playerName;
    [SerializeField] private TMP_Text _playerScore;
    [SerializeField] private GameObject _authorizationScreen;

    private void Start()
    {
        _playerName.text = PlayerPrefs.GetString("PlayerName", "");


        if (int.Parse(_playerScore.text) > 0 && _playerName.text != "")
        {
            _leaderBoard.SetLeaderboardEntry(_playerName.text, int.Parse(_playerScore.text));
        }
        else
        {
            _leaderBoard.GetLeaderboard();
        }
    }

    private void OnEnable()
    {
        _leaderBoard.OnAuthorize += Authorize;
        _leaderBoard.OnPing += TryEnter;
    }

    private void OnDisable()
    {
        _leaderBoard.OnAuthorize -= Authorize;

    }

    public void TryEnter(bool isPinging)
    {
        if (_playerName.text == "" && isPinging)
        {
            _authorizationScreen.SetActive(true);
        }
    }

    public void TryConfirm()
    {
        _leaderBoard.TryAuthorize(_inputName.text);
    }

    private void Authorize(bool confirmation)
    {
        if (confirmation && _inputName.text != "")
        {
            _playerName.text = _inputName.text;
            PlayerPrefs.SetString("PlayerName", _playerName.text);
            _authorizationScreen.SetActive(false);
        }
    }
}

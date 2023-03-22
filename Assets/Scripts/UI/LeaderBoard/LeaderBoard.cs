using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Dan.Main;
using UnityEngine.Events;
using System;

public class LeaderBoard : MonoBehaviour
{
    public event UnityAction<bool> OnAuthorize;
    public Action<bool> OnPing;

    [SerializeField] private List<TextMeshProUGUI> _names;
    [SerializeField] private List<TextMeshProUGUI> _scores;

    private string publicLeaderboardKey =
        "4a5ecd5ab1e6a6fbb32eaaf8883e82f232064352a73fa2a17ead8983e9076691";

    private void Start()
    {
        TryPing();
    }

    public void GetLeaderboard()
    {
        LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, ((msg) =>
        {
            int loopLength = (msg.Length < _names.Count) ? msg.Length : _names.Count;
            for (int i = 0; i < loopLength; i++)
            {
                _names[i].text = msg[i].Username;
                _scores[i].text = msg[i].Score.ToString();
            }
        }));
    }

    public void TryAuthorize(string name)
    {
        bool isAuthorising = true;
        LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, ((msg) =>
        {
            int loopLength = (msg.Length < _names.Count) ? msg.Length : _names.Count;
            for (int i = 0; i < loopLength; i++)
            {
                if (name == msg[i].Username)
                {
                    isAuthorising = false;
                }
            }
            OnAuthorize?.Invoke(isAuthorising);
        }));

    }

    public void SetLeaderboardEntry(string username, int score)
    {
        LeaderboardCreator.UploadNewEntry(publicLeaderboardKey, username, score, ((msg) =>
        {
            GetLeaderboard();
        }));
    }

    private void TryPing()
    {
        LeaderboardCreator.Ping(OnPing);

    }

}

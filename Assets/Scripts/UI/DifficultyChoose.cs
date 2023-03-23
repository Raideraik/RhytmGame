using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyChoose : MonoBehaviour
{
    private DifficultyScreen _difficultyScreen;

    private void Awake()
    {
        _difficultyScreen = GetComponent<DifficultyScreen>();
    }
    private void OnEnable()
    {
        ChooseSong.OnLevelChoosed += OpenDifficultyScreen;
        _difficultyScreen.OnLevelDifficultyChoosed += ChooseLevel;
    }

    private void OnDisable()
    {
        ChooseSong.OnLevelChoosed -= OpenDifficultyScreen;
        _difficultyScreen.OnLevelDifficultyChoosed -= ChooseLevel;
    }

    private void ChooseLevel(int number)
    {
        SceneFader.Instance.FadeTo(number);

    }
    private void OpenDifficultyScreen()
    {
        _difficultyScreen.OpenScreen();
    }
}

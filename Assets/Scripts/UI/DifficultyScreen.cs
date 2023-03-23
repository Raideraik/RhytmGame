using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DifficultyScreen : MonoBehaviour
{
    public event UnityAction<int> OnLevelDifficultyChoosed;

    [SerializeField] private GameObject _screen;

    public void OpenScreen()
    {
        _screen.SetActive(true);
    }
    public void ChooseDifficulty(int number)
    {
        OnLevelDifficultyChoosed?.Invoke(number);
    }
}

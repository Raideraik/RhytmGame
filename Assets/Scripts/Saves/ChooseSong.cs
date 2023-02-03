using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseSong : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Song _song;

    private void OnEnable()
    {
        _button.onClick.AddListener(ChooseLevel);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(ChooseLevel);

    }

    private void ChooseLevel()
    {
        PlayerPrefs.SetInt("ChoosedSong", _song.Id);
        SceneManager.LoadSceneAsync(1);
    }
}

using NTC.Global.Cache;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoCache
{
    [SerializeField] private Button[] _startButton;
    [SerializeField] private Button _shopButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _galleryButton;
    [SerializeField] private Button _recordButton;
    [SerializeField] private GameObject _buttonsScreen, _levelChooseScreen, _shopScreen, _settingsScreen, _difficultyScreen;

    [SerializeField] private Button[] _backButtons;

    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    protected override void OnEnabled()
    {
        for (int i = 0; i < _startButton.Length; i++)
        {
            _startButton[i].onClick.AddListener(ShowLevelScreen);
        }
        _shopButton.onClick.AddListener(ShowShopScreen);
        _settingsButton.onClick.AddListener(ShowSettingsScreen);
        _exitButton.onClick.AddListener(ExitGame);
        _galleryButton.onClick.AddListener(LoadGallery);
        _recordButton.onClick.AddListener(LoadRecorRoom);

        for (int i = 0; i < _backButtons.Length; i++)
        {
            _backButtons[i].onClick.AddListener(CloseAllScreens);
        }

        CloseAllScreens();
    }

    protected override void OnDisabled()
    {
        for (int i = 0; i < _startButton.Length; i++)
        {
            _startButton[i].onClick.RemoveListener(ShowLevelScreen);
        }

        _shopButton.onClick.RemoveListener(ShowShopScreen);
        _settingsButton.onClick.RemoveListener(ShowSettingsScreen);
        _exitButton.onClick.RemoveListener(ExitGame);
        _galleryButton.onClick.RemoveListener(LoadGallery);
        _recordButton.onClick.RemoveListener(LoadRecorRoom);

        for (int i = 0; i < _backButtons.Length; i++)
        {
            _backButtons[i].onClick.RemoveListener(CloseAllScreens);
        }
    }

    private void CloseAllScreens()
    {
        _buttonsScreen.SetActive(true);
        _levelChooseScreen.SetActive(false);
        _shopScreen.SetActive(false);
        _settingsScreen.SetActive(false);
        _difficultyScreen.SetActive(false);
    }

    private void ShowLevelScreen()
    {
        AudioEffectsControll.Instance.PlayButtonClip();
        _buttonsScreen.SetActive(false);
        _shopScreen.SetActive(false);
        _levelChooseScreen.SetActive(true);
    }

    private void ShowShopScreen()
    {
        AudioEffectsControll.Instance.PlayButtonClip();
        _buttonsScreen.SetActive(false);
        _shopScreen.SetActive(true);
    }

    private void ShowSettingsScreen()
    {
        AudioEffectsControll.Instance.PlayButtonClip();
        _buttonsScreen.SetActive(false);
        _settingsScreen.SetActive(true);
    }

    private void LoadGallery()
    {
        SceneFader.Instance.FadeTo(2);
        //SceneManager.LoadSceneAsync(2);
    }

    private void LoadRecorRoom()
    {
        SceneFader.Instance.FadeTo(3);
    }

    private void ExitGame()
    {
        AudioEffectsControll.Instance.PlayButtonClip();
        Application.Quit();
    }
}

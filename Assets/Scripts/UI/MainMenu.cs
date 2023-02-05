using NTC.Global.Cache;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoCache
{
    [SerializeField] private Button _startButton, _shopButton, _exitButton, _settingsButton;
    [SerializeField] private GameObject _buttonsScreen, _levelChooseScreen, _shopScreen, _settingsScreen;

    [SerializeField] private Button[] _backButtons;

    protected override void OnEnabled()
    {
        _startButton.onClick.AddListener(ShowLevelScreen);
        _shopButton.onClick.AddListener(ShowShopScreen);
        _settingsButton.onClick.AddListener(ShowSettingsScreen);

        for (int i = 0; i < _backButtons.Length; i++)
        {
            _backButtons[i].onClick.AddListener(CloseAllScreens);
        }

        CloseAllScreens();
    }

    protected override void OnDisabled()
    {
        _startButton.onClick.RemoveListener(ShowLevelScreen);
        _shopButton.onClick.RemoveListener(ShowShopScreen);
        _settingsButton.onClick.RemoveListener(ShowSettingsScreen);

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
    }

    private void ShowLevelScreen()
    {
        _buttonsScreen.SetActive(false);
        _levelChooseScreen.SetActive(true);
    }

    private void ShowShopScreen()
    {
        _buttonsScreen.SetActive(false);
        _shopScreen.SetActive(true);
    }

    private void ShowSettingsScreen()
    {
        _buttonsScreen.SetActive(false);
        _settingsScreen.SetActive(true);
    }
}
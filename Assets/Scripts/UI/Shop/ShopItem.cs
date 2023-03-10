using NTC.Global.Cache;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopItem : MonoCache
{
    public event UnityAction<ShopItem> OnButtonSelect;

    [SerializeField] private Button _buyButton;
    [SerializeField] private Image _picture;
    [SerializeField] private TMP_Text _buttonText;

    private PlayerSkin _playerSkin;

    protected override void OnEnabled()
    {
        _buyButton.onClick.AddListener(TryBuyItem);
    }

    protected override void OnDisabled()
    {
        _buyButton.onClick.RemoveListener(TryBuyItem);
    }

    public void SetPlayerSkin(PlayerSkin playerSkin)
    {
        _playerSkin = playerSkin;
        ConfigureItem();
    }

    private void ConfigureItem()
    {
        _picture.sprite = _playerSkin.GetPicture();

        IsBuyed();
    }

    private void TryBuyItem()
    {
        if (IsBuyed())
        {
            SetChoosed();
            PlayerPrefs.SetInt("ChoosedSkin", _playerSkin.GetID());
            AudioEffectsControll.Instance.PlayButtonClip();
            OnButtonSelect?.Invoke(this);
        }
        else if (!IsBuyed() && MainMenuScore.Instance.TrySell(_playerSkin.GetPrice()))
        {
            _buyButton.image.color = Color.white;
            _buttonText.text = "Choose";
            _playerSkin.BuySkin();
            PlayerPrefs.SetInt(_playerSkin.GetID().ToString(), 1);
            AudioEffectsControll.Instance.PlayHitClip();
        }
        else
        {
            AudioEffectsControll.Instance.PlayMissClip();
        }
    }

    private bool IsBuyed()
    {
        if (PlayerPrefs.GetInt(_playerSkin.GetID().ToString()) == 1 || _playerSkin.GetPrice() == 0)
        {
            _buyButton.image.color = Color.white;
            _buttonText.text = "Choose";
            return true;
        }
        else
        {
            _buttonText.text = _playerSkin.GetPrice().ToString();
            return false;
        }
    }

    public void ResetButton()
    {
        IsBuyed();
    }

    public void SetChoosed()
    {
        _buttonText.text = "Choosed";
        _buyButton.image.color = Color.green;
    }
}

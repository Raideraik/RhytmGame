using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private Button _buyButton;
    [SerializeField] private Image _picture;
    [SerializeField] private TMP_Text _buttonText;
    //[SerializeField] private AudioClip _errorClip, _buyClip, _chooseClip;

    private PlayerSkin _playerSkin;

    private void OnEnable()
    {
        _buyButton.onClick.AddListener(TryBuyItem);
    }

    private void OnDisable()
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
            _buttonText.text = "Choosed";
            PlayerPrefs.SetInt("ChoosedSkin", _playerSkin.GetID());
            //AudioController.Instance.PlayOnce(_chooseClip);
        }
        else if (!IsBuyed() && MainMenuScore.Instance.TrySell(_playerSkin.GetPrice()))
        {
            _buttonText.text = "Choose";
            PlayerPrefs.SetInt(_playerSkin.GetID().ToString(), 1);
            // AudioController.Instance.PlayOnce(_buyClip);
        }
        else
        {
            //AudioController.Instance.PlayOnce(_errorClip);
        }
    }

    private bool IsBuyed()
    {
        if (PlayerPrefs.GetInt(_playerSkin.GetID().ToString()) == 1 || _playerSkin.GetPrice() == 0)
        {
            _buttonText.text = "Choose";
            return true;
        }
        else
        {
            _buttonText.text = _playerSkin.GetPrice().ToString();
            return false;
        }
    }
}

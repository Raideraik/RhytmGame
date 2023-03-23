using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using NTC.Global.Cache;

public class ShopMenu : MonoCache
{
    [SerializeField] private ShopItem _template;
    [SerializeField] private List<PlayerSkin> _skins;
    [SerializeField] private Transform _shopContainer;

    private List<ShopItem> _shopItems = new List<ShopItem>();

    private void Awake()
    {
        IEnumerable<PlayerSkin> skins = from skin in _skins
                                        orderby skin.GetPrice()
                                        select skin;

        _skins = skins.ToList();
        for (int i = 0; i < _skins.Count; i++)
        {
            ShopItem item = Instantiate(_template, _shopContainer);
            item.SetPlayerSkin(_skins[i]);
            _shopItems.Add(item);
        }
    }

    protected override void OnEnabled()
    {
        for (int i = 0; i < _shopItems.Count; i++)
        {
            _shopItems[i].OnButtonSelect += ChangeSelectedItem;
        }
    }

    protected override void OnDisabled()
    {
        for (int i = 0; i < _shopItems.Count; i++)
        {
            _shopItems[i].OnButtonSelect -= ChangeSelectedItem;
        }
    }

    private void ChangeSelectedItem(ShopItem shopItem)
    {
        for (int i = 0; i < _shopItems.Count; i++)
        {
            if (_shopItems[i] == shopItem)
                _shopItems[i].SetChoosed();
            else
                _shopItems[i].ResetButton();

        }
    }
}

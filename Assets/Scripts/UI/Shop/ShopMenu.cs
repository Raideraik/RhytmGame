using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ShopMenu : MonoBehaviour
{
    [SerializeField] private ShopItem _template;
    [SerializeField] private List<PlayerSkin> _skins;
    [SerializeField] private Transform _shopContainer;

    private void Start()
    {
        IEnumerable<PlayerSkin> skins = from skin in _skins
                                        orderby skin.GetPrice()
                                        select skin;

        _skins = skins.ToList();
        for (int i = 0; i < _skins.Count; i++)
        {
            ShopItem item = Instantiate(_template, _shopContainer);
            item.SetPlayerSkin(_skins[i]);
        }
    }

}

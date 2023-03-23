using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionPanel : MonoBehaviour
{
    [SerializeField] private PlayerSkin[] _itemDatas;
    [SerializeField] private ObjectPlacer _objectPlacer;
    [SerializeField] private GalleryItem _itemTemplate;
    [SerializeField] private Transform _container;

    private void Start()
    {
        for (int i = 0; i < _itemDatas.Length; i++)
        {
            if (_itemDatas[i].IsBuyed())
            {
                AddItem(_itemDatas[i]);
            }
        }
    }

    private void AddItem(PlayerSkin playerSkin)
    {
        GalleryItem item = Instantiate(_itemTemplate, _container);
        item.Initialize(playerSkin);
        item.ItemSelected += OnItemSelected;
        item.ItemDisabled += OnItemDisabled;
    }

    private void OnItemSelected(PlayerSkin playerSkin)
    {
        _objectPlacer.SetInstalledObject(playerSkin);
    }

    private void OnItemDisabled(GalleryItem item)
    {
        item.ItemSelected -= OnItemSelected;
        item.ItemDisabled -= OnItemDisabled;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GalleryItem : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Button _selectedButton;

    private PlayerSkin _itemData;

    public event UnityAction<PlayerSkin> ItemSelected;
    public event UnityAction<GalleryItem> ItemDisabled;

    private void OnEnable()
    {
        _selectedButton.onClick.AddListener(OnSelectionButtonClicked);
    }

    private void OnDisable()
    {
        ItemDisabled?.Invoke(this);
        _selectedButton.onClick.RemoveListener(OnSelectionButtonClicked);

    }

    private void OnSelectionButtonClicked()
    {
        ItemSelected?.Invoke(_itemData);
    }

    public void Initialize(PlayerSkin itemData)
    {
        _itemData = itemData;
        _image.sprite = itemData.GetPicture();

    }

}

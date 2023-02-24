using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectButton : MonoBehaviour
{
    [SerializeField] private Image _iconImage;
    [SerializeField] private GameObject _disabledOverlay;
    [SerializeField] private Button _button;

    private CharacterSelectDisplay _characterSelect;

    public PlayerSkin Character { get; private set; }
    public bool IsDisabled { get; private set; }

    public void SetCharacter(CharacterSelectDisplay characterSelect, PlayerSkin character)
    {

        _characterSelect = characterSelect;
        Character = character;
        _iconImage.sprite = Character.GetPicture();
    }

    public void SelectCharacter()
    {
        _characterSelect.Select(Character);
    }

    public void SetDisabled()
    {
        IsDisabled = true;
        _disabledOverlay.SetActive(true);
        _button.interactable = false;
    }
}

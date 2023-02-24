using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCard : MonoBehaviour
{
    [SerializeField] private CharacterDatabase _characterDatabase;

    [SerializeField] private GameObject _visuals;
    [SerializeField] private Image _characterIconImage;
    [SerializeField] private TMP_Text _playerNameText;
    [SerializeField] private TMP_Text _characterNameText;

    public void UpdateDisplay(CharacterSelectState state)
    {
        if (state.CharacterId != -1)
        {
            var character = _characterDatabase.GetCharacterById(state.CharacterId);
            _characterIconImage.sprite = character.GetPicture();
            _characterIconImage.enabled = true;
            _characterNameText.text = character.DisplayName;
        }
        else
        {
            _characterIconImage.enabled = false;
        }

        _playerNameText.text = state.IsLockedIn ? $"Player {state.ClientId}" : $"Player {state.ClientId} (Picking...)";

        _visuals.SetActive(true);
    }

    public void DisableDisplay()
    {
        _visuals.SetActive(false);
    }
}

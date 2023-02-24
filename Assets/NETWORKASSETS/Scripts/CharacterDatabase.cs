using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character Database", menuName = "Characters/Database")]
public class CharacterDatabase : ScriptableObject
{
    [SerializeField] private PlayerSkin[] _characters = new PlayerSkin[0];

    public PlayerSkin[] GetAllCharacters() => _characters;

    public PlayerSkin GetCharacterById(int id)
    {
        foreach (var character in _characters)
        {
            if (character.NetworkId == id)
            {
                return character;
            }
        }

        return null;
    }

    public bool IsValidCharacterId(int id)
    {
        return _characters.Any(x => x.NetworkId == id);
    }
}

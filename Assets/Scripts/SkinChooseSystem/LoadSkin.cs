using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSkin : MonoBehaviour
{
    [SerializeField] private PlayerSkin[] _choosedSkins;

    public PlayerSkin GetChoosedSkin()
    {
        for (int i = 0; i < _choosedSkins.Length; i++)
        {
            if (_choosedSkins[i].GetID() == PlayerPrefs.GetInt("ChoosedSkin", 0))
            {
                return _choosedSkins[i];
            }
        }

        return null;
    }
}

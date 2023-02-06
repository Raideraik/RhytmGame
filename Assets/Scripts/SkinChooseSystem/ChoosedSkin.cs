using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoosedSkin : MonoBehaviour
{
    [SerializeField] private PlayerSkin _skin;

    public int GetID()
    {
        return _skin.GetID();
    }
}

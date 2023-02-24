using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skin", menuName = "CreatePlayerSkin", order = 1)]
public class PlayerSkin : ScriptableObject
{
    [SerializeField] private int _id;
    [SerializeField] private int _price;
    [SerializeField] private int _networkId = -1;
    [SerializeField] private Sprite _picture;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private bool _isBuyed = false;
    [SerializeField] private string _displayName = "New DisplayName";

    public int NetworkId => _networkId;
    public string DisplayName => _displayName;


    public int GetID()
    {
        return _id;
    }
    public int GetPrice()
    {
        return _price;
    }
    public Sprite GetPicture()
    {
        return _picture;
    }

    public GameObject GetPrefab()
    {
        return _prefab;
    }

    public bool IsBuyed()
    {
        return _isBuyed;
    }

    public void BuySkin()
    {
        _isBuyed = true;
    }
}

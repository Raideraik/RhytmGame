using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

[CreateAssetMenu(fileName = "Skin", menuName = "CreatePlayerSkin", order = 1)]
public class PlayerSkin : ScriptableObject
{
    [SerializeField] private int _id;
    [SerializeField] private int _price;
    [SerializeField] private Sprite _picture;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private bool _isBuyed = false;
    [SerializeField] private VisualEffect _spawnEffect;

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

    public VisualEffect GetSpawnEffect()
    {
        return _spawnEffect;
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

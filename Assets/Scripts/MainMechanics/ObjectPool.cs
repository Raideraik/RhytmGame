using NTC.Global.Cache;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoCache
{
    [SerializeField] private GameObject _container;
    [SerializeField] private int _capacity;

    private List<NoteMover> _pool = new List<NoteMover>();

    protected void Initialize(NoteMover prefab)
    {
        for (int i = 0; i < _capacity; i++)
        {
            NoteMover spawned = Instantiate(prefab, _container.transform);
            spawned.gameObject.SetActive(false);

            _pool.Add(spawned);
        }
    }
    protected bool TryGetObject(out NoteMover result)
    {
        result = _pool.FirstOrDefault(p => p.gameObject.activeSelf == false);

        return result != null;
    }
}

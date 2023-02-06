using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using NTC.Global.Cache;


[RequireComponent(typeof(ARRaycastManager))]
public class ObjectPlacer : MonoCache
{
    [SerializeField] private GameObject _marker;
    [SerializeField] private LoadSkin _character;

    private ARRaycastManager _manager;
    private GameObject _installedObject;
    private List<ARRaycastHit> _hits = new List<ARRaycastHit>();

    private void Start()
    {
        _manager = Get<ARRaycastManager>();
        _marker.SetActive(false);
    }
    protected override void Run()
    {
        _manager.Raycast(new Vector2(UnityEngine.Screen.width / 2, UnityEngine.Screen.height / 2), _hits, TrackableType.Planes);

        if (_hits.Count > 0)
        {
            _marker.transform.position = _hits[0].pose.position;
            _marker.SetActive(true);
        }
    }

    public void InstalCharacter()
    {
        Instantiate(_character.GetChoosedSkin().GetPrefab(), _hits[0].pose.position, _character.GetChoosedSkin().GetPrefab().transform.rotation);
    }

}

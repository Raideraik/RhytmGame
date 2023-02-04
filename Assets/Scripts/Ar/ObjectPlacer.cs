using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class ObjectPlacer : MonoBehaviour
{
    [SerializeField] private GameObject _marker;
    [SerializeField] private GameObject _character;

    private ARRaycastManager _manager;
    private GameObject _installedObject;
    private List<ARRaycastHit> _hits = new List<ARRaycastHit>();

    private void Start()
    {
        _manager = GetComponent<ARRaycastManager>();
        _marker.SetActive(false);
    }

    private void Update()
    {

        _manager.Raycast(new Vector2(UnityEngine.Screen.width / 2, UnityEngine.Screen.height / 2), _hits, TrackableType.Planes);

        if (_hits.Count > 0)
        {
            _marker.transform.position = _hits[0].pose.position;
            _marker.SetActive(true);
        }
    }

    public void UpdatePlacemenetPose()
    {
        Instantiate(_character, _hits[0].pose.position, _character.transform.rotation);
    }

}

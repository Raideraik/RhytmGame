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
    [SerializeField] private Transform _objectPlace;
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _container;

    private ARRaycastManager _arRaycasyManager;
    private GameObject _installedObject;
    private List<ARRaycastHit> _hits = new List<ARRaycastHit>();

    private void Start()
    {
        _arRaycasyManager = Get<ARRaycastManager>();
        _container.SetActive(false);
    }
    protected override void Run()
    {
        UpdatePlacemenetPose();

        if (Input.touchCount == 2)
        {
            SetObject();
        }
    }

    private void UpdatePlacemenetPose()
    {
        Vector2 screenCenter = _camera.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));

        var ray = _camera.ScreenPointToRay(screenCenter);

        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            SetObjectPosition(raycastHit.point);
        }
        else if (_arRaycasyManager.Raycast(screenCenter, _hits, TrackableType.PlaneWithinPolygon))
        {
            SetObjectPosition(_hits[0].pose.position);
        }
    }

    private void SetObjectPosition(Vector3 position)
    {
        _objectPlace.position = position;

        Vector3 cameraForward = _camera.transform.forward;
        Vector3 cameraRotation = new Vector3(cameraForward.x, 0, cameraForward.z);
        _objectPlace.rotation = Quaternion.Euler(cameraForward);
    }

    private void SetObject()
    {
        _installedObject.transform.parent = _container.transform;
        _installedObject = null;
    }

    public void SetInstalledObject(PlayerSkin itemData)
    {
        if (_installedObject != null)
            Destroy(_installedObject);

        _installedObject = Instantiate(itemData.GetPrefab(), _objectPlace);
    }

}

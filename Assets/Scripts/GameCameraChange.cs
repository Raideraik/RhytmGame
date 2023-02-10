using NTC.Global.Cache;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCameraChange : MonoCache
{
    [SerializeField] private StartScreen _startScreen;
    [SerializeField] private Camera _nonVRCamera, _cameraVR;
    [SerializeField] private GameObject _nonVRbackground;

    protected override void OnEnabled()
    {
        _startScreen.StartButtonClick += ChangeCameraToNonVR;
        _startScreen.SpawnCharacterButtonClikck += ChangeCameraVR;
    }

    protected override void OnDisabled()
    {
        _startScreen.StartButtonClick -= ChangeCameraToNonVR;
        _startScreen.SpawnCharacterButtonClikck -= ChangeCameraVR;
    }

    private void ChangeCameraToNonVR()
    {
        _nonVRCamera.gameObject.SetActive(true);
        _nonVRbackground.gameObject.SetActive(true);
        _cameraVR.gameObject.SetActive(false);
    }
    private void ChangeCameraVR()
    {
        _nonVRCamera.gameObject.SetActive(false);
        _nonVRbackground.gameObject.SetActive(false);
        _cameraVR.gameObject.SetActive(true);
    }
}

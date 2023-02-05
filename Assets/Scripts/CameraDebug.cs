using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraDebug : MonoBehaviour
{
    [SerializeField] private Camera _arCamera;
    [SerializeField] private TMP_Text _text;

    private void Update()
    {
        _text.text = _arCamera.fieldOfView.ToString();
        _arCamera.fieldOfView = 60f;
    }

}

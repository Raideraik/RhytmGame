using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
//using UnityEngine.XR.ARFoundation;

public class CameraDebug : MonoBehaviour
{
    public int avgFrameRate;

    [SerializeField] private TMP_Text _text;

    private void Update()
    {
        //_text.text = Application.targetFrameRate.ToString();
        //  _arCamera.fieldOfView = 60f;
        float current = 0;
        current = (int)(1f / Time.unscaledDeltaTime);
        avgFrameRate = (int)current;
        _text.text = avgFrameRate.ToString() + " FPS";

    }

}

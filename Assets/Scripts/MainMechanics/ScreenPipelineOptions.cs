using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ScreenPipelineOptions : MonoBehaviour
{
    [SerializeField] private bool _isScreenLandscape;
    [SerializeField] private RenderPipelineAsset _pipeline;
    private void Start()
    {
        Screen.orientation = ScreenOrientation.AutoRotation;

        if (_isScreenLandscape)
        {
            Screen.autorotateToLandscapeLeft = Screen.autorotateToLandscapeRight = true;
            Screen.autorotateToPortrait = Screen.autorotateToPortraitUpsideDown = false;
            GraphicsSettings.renderPipelineAsset = _pipeline;
        }
        else
        {
            Screen.autorotateToLandscapeLeft = Screen.autorotateToLandscapeRight = false;
            Screen.autorotateToPortrait = Screen.autorotateToPortraitUpsideDown = true;
            GraphicsSettings.renderPipelineAsset = null;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ScreenPipelineOptions : MonoBehaviour
{
    [SerializeField] private bool _isScreenLandscape;
    private void Start()
    {

        switch (_isScreenLandscape)
        {
            case true:
                Screen.orientation = ScreenOrientation.LandscapeLeft;
                Screen.orientation = ScreenOrientation.AutoRotation;

                Screen.autorotateToLandscapeLeft = Screen.autorotateToLandscapeRight = true;
                Screen.autorotateToPortrait = Screen.autorotateToPortraitUpsideDown = false;
                break;
            case false:
                Screen.orientation = ScreenOrientation.Portrait;
                Screen.orientation = ScreenOrientation.AutoRotation;

                Screen.autorotateToLandscapeLeft = Screen.autorotateToLandscapeRight = false;
                Screen.autorotateToPortrait = Screen.autorotateToPortraitUpsideDown = true;
                break;
        }
    }
}

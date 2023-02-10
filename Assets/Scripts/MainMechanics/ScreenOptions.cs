using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenOptions : MonoBehaviour
{
    private void Start()
    {
        Screen.orientation = ScreenOrientation.AutoRotation;

        Screen.autorotateToLandscapeLeft = Screen.autorotateToLandscapeRight = true;
        Screen.autorotateToPortrait = Screen.autorotateToPortraitUpsideDown = false;
    }
}

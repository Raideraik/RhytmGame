using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QualitySettingsMenu : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _quality;

    private void Start()
    {
        _quality.value = PlayerPrefs.GetInt("QualitySettingsPreference");
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("QualitySettingsPreference", _quality.value);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gallery : MonoBehaviour
{
    [SerializeField] private Button _backButton;

    private void OnEnable()
    {
        _backButton.onClick.AddListener(ReturnToMainMenu);
    }

    private void OnDisable()
    {
        _backButton.onClick.RemoveListener(ReturnToMainMenu);
    }

    private void Start()
    {
        
    }

    private void ReturnToMainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }
}

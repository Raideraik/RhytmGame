using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioMusic : MonoBehaviour
{
    public static AudioMusic Instance { get; private set; }


    [SerializeField] private AudioSource _effectSource;


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There more than one AudioMusic!" + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void PlayClip(AudioClip audioClip)
    {
        if (!_effectSource.isPlaying)
        {
            _effectSource.PlayOneShot(audioClip);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;

public class Mp3Loader : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    private void Start()
    {
        AudioFlow.Instance.OnSongSeted += LoadAudiox;
    }

    private void OnDisable()
    {
        AudioFlow.Instance.OnSongSeted -= LoadAudiox;
    }

    private IEnumerator LoadAudio(string path)
    {
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(path, AudioType.MPEG))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.result);
                AudioClip myClip = DownloadHandlerAudioClip.GetContent(www);
                _audioSource.clip = myClip;
            }
        }

    }

    public void LoadAudiox()
    {
        StartCoroutine(LoadAudio(AudioFlow.Instance.GetSong().GetClipAdress()));
    }


}

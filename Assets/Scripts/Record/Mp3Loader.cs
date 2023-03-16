using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;

public class Mp3Loader : MonoBehaviour
{
    public static Mp3Loader Instance { get; private set; }


    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private TMP_InputField _songName;
    [SerializeField] private TMP_Dropdown _songMenu;

    [SerializeField] private Song[] _songs;

    private string FinalPath;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There more than one Mp3Loader!" + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }


    private void Start()
    {
        SetClip(0);
    }

    public void ChooseSong()
    {
        string FileType = NativeFilePicker.ConvertExtensionToFileType("mp3");


        NativeFilePicker.Permission permission = NativeFilePicker.PickFile((path) =>
       {
           if (path == null)
           {
           }
           else
           {
               FinalPath = path;

               // StartCoroutine(LoadAudio("file://" + FinalPath, true));

               _songs[_songMenu.value].SetClip("file://" + FinalPath);
               StartCoroutine(LoadAudio(_songs[_songMenu.value].GetClip()));

           }
       }, new string[] { FileType });
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
                AudioClip myClip = DownloadHandlerAudioClip.GetContent(www);
                _audioSource.clip = myClip;
            }
        }

    }
    public void SetClip(int clipIndex)
    {
        StartCoroutine(LoadAudio(_songs[clipIndex].GetClip()));
        _songName.text = _songs[clipIndex].SongName;
        SongRecorder.Instance.SetSong(_songs[clipIndex]);
    }

    public void SetName(string name)
    {
        _songs[_songMenu.value].SetName(name);
    }
}

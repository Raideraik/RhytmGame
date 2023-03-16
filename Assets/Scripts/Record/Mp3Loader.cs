using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;

public class Mp3Loader : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    //[SerializeField] private AudioClip _audioClip;

    [SerializeField] private TMP_InputField _songName;
    [SerializeField] private TMP_Dropdown _songMenu;

    [SerializeField] private Song[] _songs;


    private AudioClip _audio;
    private string FinalPath;

    public void ChooseSong()
    {
        string FileType = NativeFilePicker.ConvertExtensionToFileType("mp3");


        NativeFilePicker.Permission permission = NativeFilePicker.PickFile((path) =>
       {
           if (path == null)
           {
               // Debug.Log("Operation cancelled");
               //_errorText.text = path;
           }
           else
           {
               FinalPath = path;
               //Debug.Log("Picked file: " + FinalPath);

               StartCoroutine(LoadAudio());
           }
       }, new string[] { FileType });
    }

    private IEnumerator LoadAudio()
    {
        WWW www = new WWW("file://" + FinalPath);
        while (!www.isDone)
            yield return null;

        string filePath = Path.Combine(Application.dataPath, "Audio.mp3");

        Debug.Log(filePath);
        //File.WriteAllBytes(filePath, www.bytes);
        _audio = www.GetAudioClip();
        //StartCoroutine(Wait());

    }

    public void PlayMusic()
    {
        _audioSource.clip = _audio;

        // StartCoroutine(LoadMus());

        _songs[_songMenu.value].SetClip(_audio);

        _songs[_songMenu.value].SetName(_songName.text);

        _audioSource.Play();
    }
}

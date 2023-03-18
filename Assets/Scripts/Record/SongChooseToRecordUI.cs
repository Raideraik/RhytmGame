using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SongChooseToRecordUI : MonoBehaviour
{
    public event UnityAction OnPlayButton;
    public event UnityAction OnRecordButton;
    public event UnityAction OnSaveButton;
    public event UnityAction OnStopButton;

    [SerializeField] private TMP_InputField _songName;
    [SerializeField] private TMP_Dropdown _songMenu;

    [SerializeField] private Button _playButton, _chooseSongButton, _recordButton, _saveButton, _stopButton;
    [SerializeField] private TMP_Text _songLog;


    [SerializeField] private Song[] _songs;
    [SerializeField] private Mp3Loader _mp3Loader;

    private string FinalPath;

    private void Start()
    {
        SetClip(0);
    }

    private void OnEnable()
    {
        _playButton.onClick.AddListener(PlayPressed);
        _chooseSongButton.onClick.AddListener(ChooseSong);
        _recordButton.onClick.AddListener(RecordPressed);
        _saveButton.onClick.AddListener(SavePressed);
        _stopButton.onClick.AddListener(StopPressed);
    }

    private void OnDisable()
    {
        _playButton.onClick.RemoveListener(PlayPressed);
        _chooseSongButton.onClick.RemoveListener(ChooseSong);
        _recordButton.onClick.RemoveListener(RecordPressed);
        _saveButton.onClick.RemoveListener(SavePressed);
        _stopButton.onClick.RemoveListener(StopPressed);
    }

    private void PlayPressed()
    {
        OnPlayButton?.Invoke();
    }

    private void StopPressed()
    {
        OnStopButton?.Invoke();
    }

    private void RecordPressed()
    {
        OnRecordButton?.Invoke();
        _songLog.text = ("Note: " + AudioFlow.Instance.GetSongPosInBeats().ToString());

    }
    private void SavePressed()
    {
        OnSaveButton?.Invoke();
        _songLog.text = ("Saved: " + _songs[_songMenu.value].Notes.Length);
    }

    private void ChooseSong()
    {
        string FileType = NativeFilePicker.ConvertExtensionToFileType("mp3");


        NativeFilePicker.Permission permission = NativeFilePicker.PickFile((path) =>
        {
            if (path != null)
            {
                FinalPath = path;

                // StartCoroutine(LoadAudio("file://" + FinalPath, true));

                _songs[_songMenu.value].SetClipAdress("file://" + FinalPath);
                _mp3Loader.LoadAudiox();

            }
        }, new string[] { FileType });
    }

    public void SetClip(int clipIndex)
    {
        //StartCoroutine(LoadAudio(_songs[clipIndex].GetClip()));
        _songName.text = _songs[clipIndex].SongName;
        SongRecorder.Instance.SetSong(_songs[clipIndex]);
    }

    public void SetName(string name)
    {
        _songs[_songMenu.value].SetSongName(name);
    }
}

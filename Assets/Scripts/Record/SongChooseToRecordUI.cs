using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SongChooseToRecordUI : MonoBehaviour
{
    public event UnityAction OnPlayButton;
    public event UnityAction OnRecordButton;
    public event UnityAction OnSaveButton;

    [SerializeField] private TMP_InputField _songName, _songUrl;
    [SerializeField] private TMP_Dropdown _songMenu;

    [SerializeField] private Button _playButton, _chooseSongButton, _chooseSongUrlButton, _recordButton, _saveButton;
    [SerializeField] private TMP_Text _songLog;
    [SerializeField] private GameObject _confirmationScreen, _songTypeScreen;
    [SerializeField] private Button _yesButton, _noButton, _urlTypeButton, _fileTypeButton;

    [SerializeField] private Song[] _songs;
    [SerializeField] private Mp3Loader _mp3Loader;

    private SongRecorder _songRecorder;
    private string _finalPath;
    private bool _isFile;

    private void Awake()
    {
        _songRecorder = GetComponent<SongRecorder>();
    }

    private void Start()
    {
        if (!_songRecorder.DevRecording)
        {

            _songTypeScreen.gameObject.SetActive(true);

            _songMenu.options.Clear();

            for (int i = 0; i < _songs.Length; i++)
            {
                _songMenu.options.Add(new TMP_Dropdown.OptionData(_songs[i].SongName));
            }

            SetClip(0);

            DisableAllUI();
        }


    }

    private void OnEnable()
    {
        _fileTypeButton.onClick.AddListener(EnableFileScreen);
        _urlTypeButton.onClick.AddListener(EnableUrlScreen);
        _playButton.onClick.AddListener(PlayPressed);
        _chooseSongButton.onClick.AddListener(ChooseSong);
        _chooseSongUrlButton.onClick.AddListener(ChooseSongURL);
        _recordButton.onClick.AddListener(RecordPressed);
        _saveButton.onClick.AddListener(SavePressed);
        _yesButton.onClick.AddListener(YesPressed);
        _noButton.onClick.AddListener(NoPressed);
    }

    private void OnDisable()
    {

        _fileTypeButton.onClick.RemoveListener(EnableFileScreen);
        _urlTypeButton.onClick.RemoveListener(EnableUrlScreen);
        _playButton.onClick.RemoveListener(PlayPressed);
        _chooseSongButton.onClick.RemoveListener(ChooseSong);
        _chooseSongUrlButton.onClick.RemoveListener(ChooseSongURL);
        _recordButton.onClick.RemoveListener(RecordPressed);
        _saveButton.onClick.RemoveListener(SavePressed);
        _yesButton.onClick.RemoveListener(YesPressed);
        _noButton.onClick.RemoveListener(NoPressed);
    }

    private void PlayPressed()
    {
        OnPlayButton?.Invoke();
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
    private void YesPressed()
    {
        if (_isFile)
        {
            _chooseSongButton.gameObject.SetActive(true);
        }
        else
        {
            _chooseSongUrlButton.gameObject.SetActive(true);
            _songUrl.gameObject.SetActive(true);
        }
        _confirmationScreen.SetActive(false);
    }

    private void NoPressed()
    {
        _confirmationScreen.SetActive(false);
    }

    private void ChooseSong()
    {
        string FileType = NativeFilePicker.ConvertExtensionToFileType("mp3");


        NativeFilePicker.Permission permission = NativeFilePicker.PickFile((path) =>
        {
            if (path != null)
            {
                _finalPath = path;

                // StartCoroutine(LoadAudio("file://" + FinalPath, true));

                _songs[_songMenu.value].SetClipAdress("file://" + _finalPath);
                _mp3Loader.LoadAudiox();

            }
        }, new string[] { FileType });

        EnableRecordUI();
    }

    private void ChooseSongURL()
    {
        _songs[_songMenu.value].SetClipAdress(_songUrl.text);
        _mp3Loader.LoadAudiox();
    }

    private void DisableAllUI()
    {
        _playButton.gameObject.SetActive(false);
        _chooseSongButton.gameObject.SetActive(false);
        _chooseSongUrlButton.gameObject.SetActive(false);
        _recordButton.gameObject.SetActive(false);
        _saveButton.gameObject.SetActive(false);
        _songName.gameObject.SetActive(false);
        _confirmationScreen.SetActive(false);
        _songUrl.gameObject.SetActive(false);
    }

    private void EnableRecordUI()
    {
        if (_songs[_songMenu.value].GetClipAdress() == "")
            return;

        _playButton.gameObject.SetActive(true);
        _recordButton.gameObject.SetActive(true);
        _saveButton.gameObject.SetActive(true);
    }

    private void EnableUrlScreen()
    {
        _isFile = false;
        _songTypeScreen.SetActive(false);
    }

    private void EnableFileScreen()
    {
        _isFile = true;

        _songTypeScreen.SetActive(false);
    }

    public void SetClip(int clipIndex)
    {
        //StartCoroutine(LoadAudio(_songs[clipIndex].GetClip()));
        DisableAllUI();
        _songName.gameObject.SetActive(true);
        _songName.text = _songs[clipIndex].SongName;
        SongRecorder.Instance.SetSong(_songs[clipIndex]);
        if (_songs[clipIndex].GetClipAdress() != "")
        {
            _confirmationScreen.SetActive(true);
        }
        else
        {
            if (_isFile)
            {
                _chooseSongButton.gameObject.SetActive(true);
            }
            else
            {
                _chooseSongUrlButton.gameObject.SetActive(true);
                _songUrl.gameObject.SetActive(true);
            }
        }
    }

    public void SetName(string name)
    {
        _songs[_songMenu.value].SetSongName(name);
    }
}

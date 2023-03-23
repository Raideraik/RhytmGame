using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Song", menuName = "CreateSong", order = 1)]
public class Song : ScriptableObject
{
    [SerializeField] private float[] _notes;
    [SerializeField] private float _bpm = 60f;
    [SerializeField] private float _beatsShownInAdvance = 3;
    [SerializeField] private AudioClip _clip;
    [SerializeField] private int _id;
    [SerializeField] private int _neededScore;
    [SerializeField] private int _neededStarsToUnlockLevel;
    [SerializeField] private string _songName;
    [SerializeField] private string _clipAdress;
    [SerializeField] private bool _isPrivate = false;
    public float[] Notes => _notes;
    public float Bpm => _bpm;
    public float BeatsShownInAdvance => _beatsShownInAdvance;
    public int NeededScore => _neededScore;
    public int Id => _id;
    public int NeededStars => _neededStarsToUnlockLevel;
    public AudioClip Clip => _clip;
    public bool IsPrivate => _isPrivate;
    public string SongName => _songName;

    public void SetNotes(float[] notes)
    {
        _notes = notes;
    }
    public void SetBPM(float bpm)
    {
        _bpm = bpm;
    }

    public void SetBSIA(float BSIA)
    {
        _beatsShownInAdvance = BSIA;
    }

    public void SetClipAdress(string clip)
    {
        _clipAdress = clip;
    }

    public void SetNeededScore(int neeededScore)
    {
        _neededScore = neeededScore;
    }

    public void SetSongName(string name)
    {
        _songName = name;
    }

    public string GetClipAdress()
    {
        return _clipAdress;
    }

}

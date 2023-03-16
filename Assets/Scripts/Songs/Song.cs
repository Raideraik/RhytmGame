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
    [SerializeField] private string _songName;
    [SerializeField] private int _neededStarsToUnlockLevel;
    public float[] Notes => _notes;
    public float Bpm => _bpm;
    public float BeatsShownInAdvance => _beatsShownInAdvance;
    public int NeededScore => _neededScore;
    public int Id => _id;
    public int NeededStars => _neededStarsToUnlockLevel;
    public AudioClip Clip => _clip;

    public string SongName => _songName;

    public void SetNotes(float[] notes)
    {
        _notes = notes;
    }

    public void SetClip(AudioClip clip) 
    {
        _clip = clip;
    }

    public void SetName(string name) 
    {
        _songName = name;
    }
}

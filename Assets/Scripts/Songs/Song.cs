using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Song", menuName = "CreateSong", order = 1)]
public class Song : ScriptableObject
{
    [SerializeField] private float[] _notes;
    [SerializeField] private float _bpm;
    [SerializeField] private float _beatsShownInAdvance;
    [SerializeField] private AudioClip _clip;
    [SerializeField] private int _id;
    [SerializeField] private int _neededScore;
    [SerializeField] private string _songName;
    public float[] Notes => _notes;
    public float Bpm => _bpm;
    public float BeatsShownInAdvance => _beatsShownInAdvance;
    public int NeededScore => _neededScore;
    public AudioClip Clip => _clip;
    public int Id => _id;

    public string SongName => _songName;

    public void SetNotes(float[] notes)
    {
        _notes = notes;
    }
}

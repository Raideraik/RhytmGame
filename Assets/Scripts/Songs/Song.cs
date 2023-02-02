using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Song", menuName = "CreateSong", order = 1)]
public class Song : ScriptableObject
{
    [SerializeField] private float[] _notes;
    [SerializeField] private float _bpm;
    [SerializeField] private AudioClip _clip;

    public float[] Notes => _notes;
    public float Bpm => _bpm;

    public AudioClip Clip => _clip;

    public void SetNotes(float[] notes)
    {
        _notes = notes;
    }
}

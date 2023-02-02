using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteMover : MonoBehaviour
{
    [SerializeField] private Vector2 SpawnPos;
    [SerializeField] private Vector2 RemovePos;

    private Spawner _spawner;
    private float beatOfThisNote;

    private void Start()
    {
        transform.position = SpawnPos;
    }

    private void Update()
    {
        transform.position = Vector2.Lerp(
            SpawnPos,
            RemovePos,
           // (_spawner.GetBeatsShownInAdvance() - (beatOfThisNote - _spawner.GetSongPosInBeats())) / _spawner.GetBeatsShownInAdvance()
           (_spawner.GetSongPosInBeats() - beatOfThisNote) / _spawner.GetBeatsShownInAdvance()
        );
    }

    public void SetSpawner(Spawner spawner)
    {
        _spawner = spawner;
    }
    public void SetBeatOfThisNote(float beat)
    {
        beatOfThisNote = beat;
    }
}

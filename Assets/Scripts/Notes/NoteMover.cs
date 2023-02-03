using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Note))]
public class NoteMover : MonoBehaviour
{
    [SerializeField] private Vector2 SpawnPos;
    [SerializeField] private Vector2 RemovePos;
    [SerializeField] private Note _note;

    private Spawner _spawner;
    private float beatOfThisNote;

    private void OnDisable()
    {
        transform.position = SpawnPos;
    }

    private void Update()
    {
        transform.position = Vector2.Lerp(
            SpawnPos,
            RemovePos,
          (_spawner.GetBeatsShownInAdvance() - (beatOfThisNote - _spawner.GetSongPosInBeats())) / _spawner.GetBeatsShownInAdvance()
        //(_spawner.GetBeatsShownInAdvance() - (_spawner.GetSongPosInBeats() - beatOfThisNote)) / _spawner.GetBeatsShownInAdvance()
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
    public void UpdateCollor()
    {
        _note.SetColor();
    }
}

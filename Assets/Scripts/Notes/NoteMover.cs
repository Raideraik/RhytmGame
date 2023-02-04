using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Note))]
public class NoteMover : MonoBehaviour
{
    [SerializeField] private Note _note;

    private Transform _spawnPos;
    private Transform _removePos;
    private Spawner _spawner;
    private float beatOfThisNote;

    private void Start()
    {
        ResetPosition();
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(
            _spawnPos.position,
            _removePos.position,
          (_spawner.GetBeatsShownInAdvance() - (beatOfThisNote - AudioFlow.Instance.GetSongPosInBeats())) / _spawner.GetBeatsShownInAdvance()
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
    }

    public void SetPositions(Transform spawn, Transform remove)
    {
        _spawnPos = spawn;
        _removePos = remove;
    }

    public void ResetPosition()
    {
        transform.position = _spawnPos.position;
        _note.SetColor();
    }
}

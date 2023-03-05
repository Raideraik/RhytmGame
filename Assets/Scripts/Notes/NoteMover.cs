using NTC.Global.Cache;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Note))]
public class NoteMover : MonoCache
{
    [SerializeField] private Note _note;
    //[SerializeField] private float _speed;

    private Transform _spawnPos;
    private Transform _finishPos;
    private Transform _removePos;
    private Spawner _spawner;
    private float beatOfThisNote;
    private bool _pointAchieved = false;
    // private float _speedToRemovePosition = 10f;

    private void Start()
    {
        ResetPosition();
    }
    protected override void LateRun()
    {
        ChoosePath();
    }
    private void ChoosePath()
    {
        if (Vector2.Distance(transform.position, _finishPos.position) > 0.1 && !_pointAchieved)
        {
            //transform.position = Vector3.MoveTowards(transform.position, _finishPos.position, Time.deltaTime * beatOfThisNote);

            transform.position = Vector2.Lerp(
            _spawnPos.position,
            _finishPos.position,
            //Time.deltaTime * _speed
            (_spawner.GetBeatsShownInAdvance() - (beatOfThisNote - AudioFlow.Instance.GetSongPosInBeats())) / _spawner.GetBeatsShownInAdvance() // / Time.deltaTime * _speed
               );

        }
        else
        {
            _pointAchieved = true;
            transform.Translate(_removePos.position.x, 0, 0);
            //transform.Translate(Vector3.down * Time.deltaTime * _speedToRemovePosition, Space.World);
        }
    }
    public void SetSpawner(Spawner spawner)
    {
        _spawner = spawner;
    }
    public void SetBeatOfThisNote(float beat)
    {
        beatOfThisNote = beat;
    }
    public void SetPositions(Transform spawn, Transform finish, Transform remove)
    {
        _spawnPos = spawn;
        _finishPos = finish;
        _removePos = remove;
    }

    public void ResetPosition()
    {
        _pointAchieved = false;
        transform.position = _spawnPos.position;
        _note.SetColor();
    }
}

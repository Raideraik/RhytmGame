using NTC.Global.Cache;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Note))]
public class NoteMover : MonoBehaviour
{
    [SerializeField] private Note _note;
    [SerializeField] private float _speed;
    [SerializeField] private bool _isRecordNote;

    private Transform _spawnPos;
    private Transform _finishPos;
    //private Transform _removePos;
    private Spawner _spawner; // comment
    private float beatOfThisNote; // comment
    private bool _pointAchieved = false;
    private float _speedToRemovePosition = 10f;


    private void Start()
    {
        ResetPosition();
    }
    private void Update()
    {
        ChoosePath();
    }
    private void ChoosePath()
    {

        if (Vector3.Distance(transform.position, _finishPos.position) > 0.1 && !_pointAchieved)
        {

            /*
            transform.position = Vector3.Lerp(
            _spawnPos.position,
            _finishPos.position,
               // (beatOfThisNote * AudioFlow.Instance.GetSongPosInBeats()) / Time.deltaTime
               (_spawner.GetBeatsShownInAdvance() - (beatOfThisNote - AudioFlow.Instance.GetSongPosInBeats())) / _spawner.GetBeatsShownInAdvance() // / Time.deltaTime * _speed
               );*/

            if (!_isRecordNote)
            {
                transform.position = Vector2.MoveTowards(
            transform.position,
           _finishPos.position,
              // (beatOfThisNote * AudioFlow.Instance.GetSongPosInBeats()) / Time.deltaTime
              _speed * Time.deltaTime // / Time.deltaTime * _speed
              );



                // transform.Translate(Vector3.left * Time.deltaTime * _speed, Space.World);
                //transform.Translate(Vector3.left * _speed * Time.deltaTime, Space.World);


            }
            else
            {
                transform.Translate(Vector3.up * Time.deltaTime * _speed, Space.World);
            }


        }
        else
        {
            _pointAchieved = true;
            // transform.Translate(_removePos.position.x, 0, 0);
            if (!_isRecordNote)
            {
                transform.Translate(Vector3.left * Time.deltaTime * _speedToRemovePosition, Space.World);

            }
            else
            {
                transform.Translate(Vector3.up * Time.deltaTime * _speedToRemovePosition, Space.World);

            }
        }
    }

    public void SetSpawner(Spawner spawner)
    {
        _spawner = spawner;// comment
    }
    public void SetBeatOfThisNote(float beat)
    {
        beatOfThisNote = beat;                    // comment
    }
    public void SetPositions(Transform spawn, Transform finish)//, Transform remove)
    {
        _spawnPos = spawn;
        _finishPos = finish;
        //_removePos = remove;

    }

    public void SetIsRecord(bool isRecord)
    {
        _isRecordNote = isRecord;
    }

    public void ResetPosition()
    {
        _pointAchieved = false;
        transform.position = _spawnPos.position;
        _note.SetColor();

    }

}

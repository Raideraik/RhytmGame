using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class RecordVisualize : ObjectPool
{
    [SerializeField] private NoteMover _template;
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private Transform _finishPosition;
    [SerializeField] private SongChooseToRecordUI _recordUI;
    protected override void OnEnabled()
    {
        _recordUI.OnRecordButton += SpawnNote;
    }
    protected override void OnDisabled()
    {
        _recordUI.OnRecordButton -= SpawnNote;

    }

    private void Start()
    {
        Initialize(_template);
    }

    private void SpawnNote()
    {
        if (TryGetObject(out NoteMover note))
        {
            note.gameObject.SetActive(true);
            note.SetPositions(_spawnPosition, _finishPosition);
            note.SetIsRecord(true);
        }
    }
}

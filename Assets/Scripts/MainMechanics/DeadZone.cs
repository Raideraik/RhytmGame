using NTC.Global.Cache;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DeadZone : MonoCache
{
    public event UnityAction OnDeadZone;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Note note))
        {
            note.ResetNote();
            OnDeadZone?.Invoke();
        }
    }
}

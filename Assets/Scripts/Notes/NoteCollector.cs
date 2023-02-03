using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NoteCollector : MonoBehaviour
{
    public event UnityAction<Note> OnTriggetEnter;
    public event UnityAction OnTriggerExit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Note note))
        {
            OnTriggetEnter?.Invoke(note);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        OnTriggerExit?.Invoke();
    }
}

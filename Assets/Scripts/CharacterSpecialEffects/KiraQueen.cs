using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KiraQueen : Character
{
    private void Start()
    {
        for (int i = 0; i < _effects.Length; i++)
        {
            NoteVisualEffects.Instance.AddEffect(_effects[i]);
        }
    }
}

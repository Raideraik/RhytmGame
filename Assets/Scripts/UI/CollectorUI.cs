using NTC.Global.Cache;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollectorUI : MonoCache
{
    [SerializeField] private TMP_Text _phraseText;
    [SerializeField] private CollectorController[] _collector;

    protected override void OnEnabled()
    {
        for (int i = 0; i < _collector.Length; i++)
        {
            _collector[i].OnCollected += OnCollected;
            _collector[i].OnWrong += OnWrong;
        }
    }
    protected override void OnDisabled()
    {
        for (int i = 0; i < _collector.Length; i++)
        {
            _collector[i].OnCollected -= OnCollected;
            _collector[i].OnWrong -= OnWrong;
        }
    }
    private void OnCollected()
    {
        _phraseText.color = Color.green;
        _phraseText.text = "Good!";
    }
    private void OnWrong()
    {
        _phraseText.color = Color.red;
        _phraseText.text = "Bad!";
    }
}

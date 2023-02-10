using NTC.Global.Cache;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollectorUI : MonoCache
{
    [SerializeField] private TMP_Text _phraseText;
    [SerializeField] private CollectorController[] _collector;
    [SerializeField] private Animator _phraseAnimator;

    [SerializeField] private string[] _goodPhrases;
    [SerializeField] private string[] _badPhrases;
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
        //_phraseText.color = Color.green;
        _phraseAnimator.Play("GoodCollectorPhrase");
        _phraseText.text = _goodPhrases[Random.Range(0, _goodPhrases.Length)];
    }
    private void OnWrong()
    {
        _phraseAnimator.Play("BadCollectorPhrase");
        _phraseText.text = _badPhrases[Random.Range(0, _badPhrases.Length)];
    }
}

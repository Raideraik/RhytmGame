using NTC.Global.Cache;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollectorUI : MonoCache
{
    [SerializeField] private TMP_Text _phraseText;
    //[SerializeField] private CollectorController[] _collector;
    //[SerializeField] private ControllersHandler _controllersHandler;
    [SerializeField] private Animator _phraseAnimator;

    [SerializeField] private string[] _goodPhrases;
    [SerializeField] private string[] _badPhrases;
    protected override void OnEnabled()
    {
        ControllersHandler.OnAnyCollected += OnCollected;
        ControllersHandler.OnAnyMissed += OnWrong;
    }
    protected override void OnDisabled()
    {
        ControllersHandler.OnAnyCollected -= OnCollected;
        ControllersHandler.OnAnyMissed -= OnWrong;
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

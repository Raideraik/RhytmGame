using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollectorUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _phraseText;
    [SerializeField] private CollectorController[] _collector;

    private void OnEnable()
    {
        for (int i = 0; i < _collector.Length; i++)
        {
            _collector[i].OnCollected += OnCollected;
            _collector[i].OnWrong += OnWrong;
        }

    }

    private void OnDisable()
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

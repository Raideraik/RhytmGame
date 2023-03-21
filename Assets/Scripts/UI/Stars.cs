using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Stars : MonoBehaviour
{
    public static Stars Instance { get; private set; }

    [SerializeField] private TMP_Text _starsText;

    private int _starsCount;
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There more than one Stars!" + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void AddStars(int starsToAdd)
    {
        _starsCount += starsToAdd;
    }

    public int GetStars()
    {
        return _starsCount;
    }

    public void UpdateStars()
    {
        _starsText.text = _starsCount.ToString();
    }
}

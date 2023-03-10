using NTC.Global.Cache;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Collor
{
    Red,
    Green,
    Blue,
    Yellow
}
public class Note : MonoCache
{
    [SerializeField] private Material _redNoteMaterial;
    [SerializeField] private Material _greenNoteMaterial;
    [SerializeField] private Material _blueNoteMaterial;
    [SerializeField] private Material _yellowNoteMaterial;

    [SerializeField] private Sprite _redNoteSprite;
    [SerializeField] private Sprite _greenNoteSprite;
    [SerializeField] private Sprite _blueNoteSprite;
    [SerializeField] private Sprite _yellowNoteSprite;

    private Collor _color;
    private SpriteRenderer _spriteRenderer;

    private NoteMover _mover;
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _mover = GetComponent<NoteMover>();
        // SetColor();
    }

    public void SetColor()
    {
        switch (Random.Range(0, 4))
        {
            case 0:
                _color = Collor.Yellow;
                _spriteRenderer.sprite = _yellowNoteSprite;
                _spriteRenderer.material = _yellowNoteMaterial;
                break;
            case 1:
                _color = Collor.Red;
                _spriteRenderer.sprite = _redNoteSprite;
                _spriteRenderer.material = _redNoteMaterial;
                break;
            case 2:
                _color = Collor.Green;
                _spriteRenderer.sprite = _greenNoteSprite;
                _spriteRenderer.material = _greenNoteMaterial;
                break;
            case 3:
                _color = Collor.Blue;
                _spriteRenderer.sprite = _blueNoteSprite;
                _spriteRenderer.material = _blueNoteMaterial;
                break;
        }
    }
    public Collor GetColor()
    {
        return _color;
    }

    public void ResetNote()
    {
        _mover.ResetPosition();
        gameObject.SetActive(false);
    }
}

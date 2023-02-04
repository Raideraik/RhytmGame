using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Collor
{
    Red,
    Green,
    Blue,
    Yellow
}
public class Note : MonoBehaviour
{
    private Collor _color;
    private SpriteRenderer _spriteRenderer;

    [SerializeField] private Sprite _redNoteSprite;
    [SerializeField] private Sprite _greenNoteSprite;
    [SerializeField] private Sprite _blueNoteSprite;
    [SerializeField] private Sprite _yellowNoteSprite;

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
                break;
            case 1:
                _color = Collor.Red;
                _spriteRenderer.sprite = _redNoteSprite;
                break;
            case 2:
                _color = Collor.Green;
                _spriteRenderer.sprite = _greenNoteSprite;
                break;
            case 3:
                _color = Collor.Blue;
                _spriteRenderer.sprite = _blueNoteSprite;
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

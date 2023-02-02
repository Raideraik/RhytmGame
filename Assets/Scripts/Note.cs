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

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        SetColor();
    }
    private void SetColor()
    {
        switch (Random.Range(0, 4))
        {
            case 0:
                _color = Collor.Yellow;
                _spriteRenderer.color = Color.yellow;
                break;
            case 1:
                _color = Collor.Red;
                _spriteRenderer.color = Color.red;
                break;
            case 2:
                _color = Collor.Green;
                _spriteRenderer.color = Color.green;
                break;
            case 3:
                _color = Collor.Blue;
                _spriteRenderer.color = Color.blue;
                break;
        }
    }
    public Collor GetColor()
    {
        return _color;
    }
}

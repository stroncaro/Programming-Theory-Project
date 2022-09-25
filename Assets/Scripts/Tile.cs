using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private SpriteRenderer _sprite;

    private int _colorIndex; //0 is light, 1 is dark
    [SerializeField] private Color[] _baseColors = new Color[2];

    public int colorIndex
    {
        get { return _colorIndex; }
        set
        {
            if (value != 0 && value != 1)
            {
                Debug.LogErrorFormat("Tile colorIndex must be 0 or 1. Got {0}", value);
                return;
            }

            _colorIndex = value;
            SetColor(_baseColors[_colorIndex]);
        }
    }

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void SetColor(Color value)
    {
        _sprite.color = value;
    }
}

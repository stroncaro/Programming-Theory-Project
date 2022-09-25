using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private SpriteRenderer _sprite;

    private int _colorIndex;
    [SerializeField] private Color[] baseColors = new Color[2]; //0 is light, 1 is dark

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
            this.SetColor(baseColors[_colorIndex]);
        }
    }

    
    

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void SetColor(Color value)
    {
        this._sprite.color = value;
    }
}

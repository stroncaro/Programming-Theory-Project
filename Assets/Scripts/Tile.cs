using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private SpriteRenderer _sprite;
    public Color color { get => this._sprite.color; set => this._sprite.color = value; }

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }
}

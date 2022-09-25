using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GUITextOutput : MonoBehaviour
{
    private TextMeshPro[,] _output;

    [SerializeField] private float _cellWidth = 0.5f;
    [SerializeField] private float _fontSize = 6;
    [SerializeField] private TextAlignmentOptions _alignment = TextAlignmentOptions.Center;

    void Awake() => Initialize();

    private void Initialize()
    {
        _output = new TextMeshPro[10, 10];

        for (int x = 0; x < _output.GetLength(0); x++)
        {
            for (int y = 0; y < _output.GetLength(1); y++)
            {
                var newObj = new GameObject(string.Format("BoardPosition:{0},{1}", x, y));
                newObj.transform.position = new Vector2(x * _cellWidth, y * _cellWidth);
                newObj.transform.SetParent(transform);

                newObj.AddComponent<TextMeshPro>();
                var newObjTMPro = newObj.GetComponent<TextMeshPro>();
                newObjTMPro.text = "_";
                newObjTMPro.fontSize = _fontSize;
                newObjTMPro.alignment = _alignment;

                var newObjRectTransform = newObj.GetComponent<RectTransform>();
                newObjRectTransform.sizeDelta = new Vector2(_cellWidth, _cellWidth);

                _output[x, y] = newObjTMPro;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GUITextOutput : MonoBehaviour
{
    private TextMeshPro[,] _output;

    // Start is called before the first frame update
    void Start()
    {
        _output = new TextMeshPro[10, 10];

        for (int x = 0; x < _output.GetLength(0); x++)
        {
            for (int y = 0; y <_output.GetLength(1); y++)
            {
                var obj = new GameObject(string.Format("BoardPosition:{0},{1}", x, y));
                obj.transform.position = new Vector2(x, y);
                obj.transform.SetParent(transform);

                obj.AddComponent<TextMeshPro>();
                var tmp = obj.GetComponent<TextMeshPro>();
                
                tmp.text = "<mspace=1>_</mspace>";
                tmp.fontSize = 6;
                tmp.alignment = TextAlignmentOptions.Center;
                _output[x, y] = tmp;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

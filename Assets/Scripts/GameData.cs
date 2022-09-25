using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Instance;

    [SerializeField] private Vector2 _boardSize;
    public Board board { get; private set; }

    void Initialize()
    {
        if (Instance != null)
        {
            Debug.LogWarning("There can only be one GameData instance");
            return;
        }

        Instance = this;
        board = new Board((int)_boardSize.x, (int)_boardSize.y);
    }

    void Awake() => Initialize();
}

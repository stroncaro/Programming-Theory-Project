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

        StartCoroutine(RandomizeBoardContent());
    }

    IEnumerator RandomizeBoardContent()
    {
        var delay = new WaitForSeconds(0.1f);
        while (true)
        {
            int x = Random.Range(0, board.xWidth);
            int y = Random.Range(0, board.yWidth);
            board.contents[x, y] = !board.contents[x, y];
            yield return delay;
        }
    }

    void Awake() => Initialize();
}

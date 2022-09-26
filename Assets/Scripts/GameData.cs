using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Instance;

    [SerializeField] private Vector2 _boardSize;
    private Board _board;
    public Board board { get => _board; }

    private List<Entity> _entities = new List<Entity>();
    public List<Entity> entities { get => _entities; }

    void Initialize()
    {
        if (Instance != null)
        {
            Debug.LogWarning("There can only be one GameData instance");
            return;
        }

        Instance = this;
        _board = new Board((int)_boardSize.x, (int)_boardSize.y);

        StartCoroutine(RandomizeBoardContent());
    }

    IEnumerator RandomizeBoardContent()
    {
        var delay = new WaitForSeconds(0.1f);
        while (true)
        {
            int x = Random.Range(0, _board.rows);
            int y = Random.Range(0, _board.files);
            var thisTile = _board.tiles[x, y];
            int d2 = Random.Range(0, 2);
            switch (d2)
            {
                case 0:
                    if (thisTile.hasEntities)
                    {
                        int id = thisTile.entityIds[0];
                        var thisEntity = _entities.Find(entity => entity.id == id);
                        thisEntity.Kill();
                    }
                    else
                    {
                        var newEntity = new Entity("name", x, y);
                    }
                    break;
                case 1:
                    thisTile.isActive = !thisTile.isActive;
                    break;
            }
            
            yield return delay;
        }
    }

    void Awake() => Initialize();
}

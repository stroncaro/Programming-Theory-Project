using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Instance;

    [SerializeField] private Vector2 _boardSize;
    private Board _board;
    public static Board GetBoard() => Instance._board;

    private List<Entity> _entities = new List<Entity>();
    public static void AddEntity(Entity entity) => Instance._entities.Add(entity);
    public static void RemoveEntity(Entity entity) => Instance._entities.Remove(entity);
    public static Entity GetEntityById(int id) => Instance._entities.Find(entity => entity.id == id);

    void Initialize()
    {
        if (Instance != null)
        {
            Debug.LogWarning("There can only be one GameData instance");
            return;
        }

        Instance = this;
        _board = new Board((int)_boardSize.x, (int)_boardSize.y);

        //StartCoroutine(RandomizeBoardContent());
    }

    IEnumerator RandomizeBoardContent()
    {
        var delay = new WaitForSeconds(0.1f);
        while (true)
        {
            int x = Random.Range(0, _board.rows);
            int y = Random.Range(0, _board.files);
            var thisTile = _board.GetTile(x, y);
            int d2 = Random.Range(0, 2);
            switch (d2)
            {
                case 0:
                    if (thisTile.hasEntities)
                    {
                        var thisEntity = thisTile.GetEntity();
                        thisEntity.Kill();
                    }
                    else
                    {
                        int newD2 = Random.Range(0, 2);
                        switch (newD2)
                        {
                            case 0:
                                new Trap("name", x, y);
                                break;
                            case 1:
                                int d4 = Random.Range(0, 4);
                                Direction.World initDir = (Direction.World)d4;
                                new Avatar("name", x, y, initDir);
                                break;
                        }
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

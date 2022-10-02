using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Avatar avatar;
    [SerializeField] private Vector2 startPos;
    [SerializeField] private Direction.World facingDirection;

    void Start()
    {
        avatar = new Avatar("Player", (int)startPos.x, (int)startPos.y, facingDirection);
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(KeyCode.Q)) { avatar.RotateCounterclockwise(); }
            if (Input.GetKeyDown(KeyCode.E)) { avatar.RotateClockwise(); }
            if (Input.GetKeyDown(KeyCode.W)) { avatar.MoveForward(); }
            if (Input.GetKeyDown(KeyCode.A)) { avatar.MoveLeft(); }
            if (Input.GetKeyDown(KeyCode.S)) { avatar.MoveBack(); }
            if (Input.GetKeyDown(KeyCode.D)) { avatar.MoveRight(); }
            if (Input.GetKeyDown(KeyCode.R))
            {
                Vector2 trapDirection = avatar.forward;
                Vector2 trapPos = avatar.position + trapDirection;
                if (GameData.GetBoard().IsInBoard(trapPos))
                    new Trap("Trap", (int)trapPos.x, (int)trapPos.y);
            }
        }
    }
}

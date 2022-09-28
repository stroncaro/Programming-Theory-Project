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
        //create an avatar
        avatar = new Avatar("player", (int)startPos.x, (int)startPos.y, facingDirection);
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
        }
    }
}

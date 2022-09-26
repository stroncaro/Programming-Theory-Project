using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Avatar avatar;
    [SerializeField] private Vector2 startPos;
    [SerializeField] private Board.Direction startDirection;

    void Start()
    {
        //create an avatar
        avatar = new Avatar("player", (int)startPos.x, (int)startPos.y, startDirection);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(KeyCode.Q)) { avatar.RotateCounterclockwise(); }
            if (Input.GetKeyDown(KeyCode.E)) { avatar.RotateClockwise(); }
        }
    }
}

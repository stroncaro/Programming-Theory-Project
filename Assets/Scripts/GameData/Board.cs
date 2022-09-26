using System;
using UnityEngine;

public class Board
{
    public enum Direction { UP, RIGHT, DOWN, LEFT }

    public static Direction Rotation(Direction origin, int steps)
    {
        int cap = Enum.GetNames(typeof(Direction)).Length;
        int newDirInt = ((int)origin + steps) % cap;
        if (newDirInt < 0) { newDirInt += cap; }
        return (Direction)newDirInt;
    }

    //number of rows and files determine size of board
    private int _rows, _files;
    public int rows { get => _rows; }
    public int files { get => _files; }
    
    //in each coordinate there is a tile
    private Tile[,] _tiles;
    public Tile[,] tiles { get => _tiles; }

    public Board(int rows, int files)
    {
        //set board size
        _rows = rows;
        _files = files;

        //create board of proper width and height
        _tiles = new Tile[_rows, _files];

        //initialize tiles
        for (int i = 0; i < _rows * _files; i++) { _tiles[i / _files, i % _files] = new Tile(); }
    }
}

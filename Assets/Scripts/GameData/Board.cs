public class Board
{
    private int _xWidth, _yWidth;
    private bool[,] _board;

    public Board(int xWidth, int yWidth)
    {
        _xWidth = xWidth;
        _yWidth = yWidth;
        _board = new bool[_xWidth, _yWidth];
    }
}

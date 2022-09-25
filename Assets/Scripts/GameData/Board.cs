public class Board
{
    private int _xWidth, _yWidth;
    public int xWidth { get => _xWidth; }
    public int yWidth { get => _yWidth; }
    
    private bool[,] _contents;
    public bool[,] contents { get => _contents; }

    public Board(int xWidth, int yWidth)
    {
        _xWidth = xWidth;
        _yWidth = yWidth;
        _contents = new bool[_xWidth, _yWidth];
    }
}

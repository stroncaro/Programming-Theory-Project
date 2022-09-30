using UnityEngine;

public class GUIBoardPosition : MonoBehaviour
{
    public bool mouseOver = false;

    private void OnMouseEnter() => mouseOver = true;
    private void OnMouseExit() => mouseOver = false;
}

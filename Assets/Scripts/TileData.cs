using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileData : MonoBehaviour, IPointerClickHandler
{
    public int row;
    public int column;

    public void SetValues(int x, int y)
    {
        row = x;
        column = y;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Tile X: " + row + " Y: " + column);
    }
}

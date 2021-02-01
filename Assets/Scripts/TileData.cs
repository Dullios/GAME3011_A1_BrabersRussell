using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileData : MonoBehaviour, IPointerClickHandler
{
    [Header("Grid Position")]
    public int row;
    public int column;

    [Header("Resource Info")]
    private SpotSpawner spotDetails;


    public void SetValues(int x, int y)
    {
        row = x;
        column = y;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Tile X: " + row + " Y: " + column);

        switch(TileManager.Instance.mode)
        {
            case MineMode.Scan:
                break;
            case MineMode.Extract:
                break;
        }
    }
}

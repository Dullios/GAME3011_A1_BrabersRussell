using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TileData : MonoBehaviour, IPointerClickHandler
{
    [Header("Grid Position")]
    public int row;
    public int column;

    [Header("Resource Info")]
    public int resourceCount;

    public void Initialize(int x, int y)
    {
        row = x;
        column = y;

        TileManager.Instance.OnMiniGameStart.AddListener(OnMiniGameStartAction);
    }

    public void SetResource(int resource)
    {
        resourceCount = resource;
    }

    private void OnMiniGameStartAction()
    {
        resourceCount = TileManager.Instance.activeSpot.resourceSpread[row, column].resourceCount;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log("Tile X: " + row + " Y: " + column);

        switch(TileManager.Instance.mode)
        {
            case MineMode.Scan:
                if (TileManager.Instance.activeSpot.scanCount > 0)
                {
                    TileManager.Instance.RevealTiles(row, column);
                    TileManager.Instance.activeSpot.scanCount--;
                }
                break;

            case MineMode.Extract:

                break;
        }
    }

    public void HighlightTile()
    {
        int maxResouces = TileManager.Instance.activeSpot.maxResourceValue;
        Color color = new Color(0, 0, 0);

        if(resourceCount == maxResouces)
        {
            color = new Color(1, 0, 0);
        }
        else if(resourceCount == maxResouces / 2)
        {
            color = new Color(1, 0.64f, 0);
        }
        else if(resourceCount == maxResouces / 4)
        {
            color = new Color(1, 1, 0);
        }
        else
        {
            color = new Color(0.75f, 0.75f, 0.75f);
        }

        GetComponent<Image>().color = color;
    }
}

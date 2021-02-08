using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotData : MonoBehaviour
{
    public struct ResourceView
    {
        public int resourceCount;
        public bool isRevealed;
    }

    [Header("Grid Controls")]
    public int scanCount;
    public int extractCount;
    public int maxResourceValue;
    public int hotSpotCount;

    [Header("Tile Values")]
    public ResourceView[,] resourceSpread;
    public int rows;
    public int columns;


    // Start is called before the first frame update
    void Start()
    {
        maxResourceValue = 1000 * Random.Range(2, 6);

        rows = TileManager.Instance.rows;
        columns = TileManager.Instance.columns;
        resourceSpread = new ResourceView[rows, columns];

        PopulateResources();
    }

    private void PopulateResources()
    {
        for(int r = 0; r < TileManager.Instance.rows; r++)
        {
            for(int c = 0; c < TileManager.Instance.columns; c++)
            {
                resourceSpread[r, c].resourceCount = 100;
                resourceSpread[r, c].isRevealed = false;
            }
        }

        RandomizeHotSpots();
    }

    private void RandomizeHotSpots()
    {
        for(int i = 0; i < hotSpotCount; i++)
        {
            int x = Random.Range(0, rows);
            int y = Random.Range(0, columns);

            resourceSpread[x, y].resourceCount = maxResourceValue;
            FillHotSpots(x, y);
        }
    }

    private void FillHotSpots(int x, int y)
    {
        for (int r = -2; r < 3; r++)
        {
            int xOffset = x + r;
            
            for (int c = -2; c < 3; c++)
            {
                int yOffset = y + c;

                // Skip if the values would run off the grid
                if (xOffset < 0 || yOffset < 0)
                    continue;
                else if (xOffset >= rows || yOffset >= columns)
                    continue;

                if (xOffset == x && yOffset == y)
                {
                    resourceSpread[r, c].resourceCount = maxResourceValue;
                }
                else if ((r == -2 || r == 2) || (c == -2 || c == 2))
                {
                    resourceSpread[xOffset, yOffset].resourceCount = maxResourceValue / 4;
                }
                else
                {
                    resourceSpread[xOffset, yOffset].resourceCount = maxResourceValue / 2;
                }
            }
        }
    }
}

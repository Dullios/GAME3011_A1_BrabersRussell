using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TileManager : MonoBehaviour
{
    private static TileManager instance;

    public static TileManager Instance
    {
        get
        {
            return instance;
        }
    }

    [Header("Tile Grid")]
    public GameObject[,] tileGrid;
    public MineMode mode;

    [Header("Generation")]
    public GameObject tilePrefab;
    public int rows;
    public int columns;
    public float tileWidth, tileHeight;
    public float horiPadding, vertPadding;

    [Header("Active Spot")]
    public SpotData activeSpot;

    [Header("Events")]
    public UnityEvent OnMiniGameStart;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        tileGrid = new GameObject[rows, columns];
        mode = MineMode.Scan;

        for(int r = 0; r < rows; r++)
        {
            for(int c = 0; c < columns; c++)
            {
                GameObject t = GameObject.Instantiate(tilePrefab, gameObject.transform.GetChild(0));

                int cPos = c + 1;
                int rPos = r + 1;
                t.GetComponent<RectTransform>().anchoredPosition = new Vector2(cPos * tileWidth + (cPos * horiPadding), -(rPos * tileHeight + (rPos * vertPadding)));
                t.GetComponent<RectTransform>().sizeDelta = new Vector2(tileWidth, tileHeight);
                t.GetComponent<TileData>().Initialize(r, c);
                tileGrid[r, c] = t;
            }
        }

        gameObject.SetActive(false);
    }

    public void StartMiniGame(SpotData spot)
    {
        activeSpot = spot;
        OnMiniGameStart.Invoke();
    }

    public void RevealTiles(int x, int y)
    {
        for (int r = -1; r < 2; r++)
        {
            int xOffset = x + r;

            for (int c = -1; c < 2; c++)
            {
                int yOffset = y + c;

                tileGrid[xOffset, yOffset].GetComponent<TileData>().HighlightTile();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    public int totalHetzahrim = 0;

    [Header("Tile Grid")]
    public GameObject[,] tileGrid;
    public MineMode mode;

    [Header("UI Objects")]
    public TextMeshProUGUI hetzahrimCounter;
    public TextMeshProUGUI scanCounter;
    public TextMeshProUGUI mineCounter;

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
    public UnityEvent OnMiniGameEnd;

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
        mode = MineMode.Extract;

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
        scanCounter.text = "Scans: " + activeSpot.scanCount;
        mineCounter.text = "Mines: " + activeSpot.extractCount;

        OnMiniGameStart.Invoke();
    }

    public void ExtractTiles(int x, int y)
    {
        for (int r = -1; r < 2; r++)
        {
            int xOffset = x + r;

            for (int c = -1; c < 2; c++)
            {
                int yOffset = y + c;

                if(xOffset == x && yOffset == y)
                {
                    UpdateHetzahrim(tileGrid[xOffset, yOffset].GetComponent<TileData>().resourceCount);
                    tileGrid[xOffset, yOffset].GetComponent<TileData>().resourceCount = 100;
                }
                else
                {
                    UpdateHetzahrim(tileGrid[xOffset, yOffset].GetComponent<TileData>().resourceCount);
                    tileGrid[xOffset, yOffset].GetComponent<TileData>().resourceCount /= 2;

                    if (tileGrid[xOffset, yOffset].GetComponent<TileData>().resourceCount < activeSpot.maxResourceValue / 4)
                    {
                        tileGrid[xOffset, yOffset].GetComponent<TileData>().resourceCount = 100;
                    }
                }

                tileGrid[xOffset, yOffset].GetComponent<TileData>().HighlightTile();
                
                activeSpot.resourceSpread[xOffset, yOffset].resourceCount = tileGrid[xOffset, yOffset].GetComponent<TileData>().resourceCount;
                activeSpot.resourceSpread[xOffset, yOffset].isRevealed = true;
            }
        }
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

    public void UpdateHetzahrim(int count)
    {
        totalHetzahrim += count;

        hetzahrimCounter.text = "Hetzahrim: " + totalHetzahrim.ToString("000000");
    }

    public void UpdateScans()
    {
        activeSpot.scanCount--;
        scanCounter.text = "Scans: " + activeSpot.scanCount;
    }

    public void UpdateMines()
    {
        activeSpot.extractCount--;
        mineCounter.text = "Mines: " + activeSpot.extractCount;

        if(activeSpot.extractCount <= 0)
        {
            StartCoroutine(MiniGameEndCoroutine());
        }
    }

    IEnumerator MiniGameEndCoroutine()
    {
        yield return new WaitForSeconds(2);

        OnMiniGameEnd.Invoke();

        activeSpot.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}

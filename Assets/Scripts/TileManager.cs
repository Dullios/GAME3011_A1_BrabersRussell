using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [Header("Generation")]
    public GameObject tilePrefab;
    public int rows;
    public int columns;
    public float tileWidth, tileHeight;
    public float horiPadding, vertPadding;

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

        for(int r = 0; r < rows; r++)
        {
            for(int c = 0; c < columns; c++)
            {
                GameObject t = GameObject.Instantiate(tilePrefab, gameObject.transform.GetChild(0));

                int cPos = c + 1;
                int rPos = r + 1;
                t.GetComponent<RectTransform>().anchoredPosition = new Vector2(cPos * tileWidth + (cPos * horiPadding), -(rPos * tileHeight + (rPos * vertPadding)));
                t.GetComponent<RectTransform>().sizeDelta = new Vector2(tileWidth, tileHeight);
                t.GetComponent<TileData>().SetValues(r, c);
                tileGrid[r, c] = t;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

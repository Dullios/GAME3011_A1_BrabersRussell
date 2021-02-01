using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotData : MonoBehaviour
{
    [Header("Grid Values")]
    public int scanCount;
    public int extractCount;
    public int maxResourceValue;

    // Start is called before the first frame update
    void Start()
    {
        scanCount = 6;
        extractCount = 3;
        maxResourceValue = 1000 * Random.Range(2, 6);
    }
}

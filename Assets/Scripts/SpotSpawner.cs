using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotSpawner : MonoBehaviour
{
    private SpotSpawner instance;

    [Header("Spawn Details")]
    public GameObject spotPrefab;
    public float minX, maxX;
    public float minY, maxY;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

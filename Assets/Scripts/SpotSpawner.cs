﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotSpawner : MonoBehaviour
{
    private SpotSpawner instance;

    [Header("Spawn Details")]
    public GameObject spotPrefab;
    public int numberOfSpots;
    public float minX, maxX;
    public float minZ, maxZ;

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
        for(int i = 0; i < numberOfSpots; i++)
        {
            Vector3 pos = new Vector3(Random.Range(minX, maxX), 0, Random.Range(minZ, maxZ));
            GameObject.Instantiate(spotPrefab, pos, Quaternion.identity, gameObject.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

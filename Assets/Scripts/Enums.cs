using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum GameState
{
    MainGame,
    MiniGame
}

[System.Serializable]
public enum MineMode
{
    Scan,
    Extract
}
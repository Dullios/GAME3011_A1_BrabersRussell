using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleModeScript : MonoBehaviour
{
    private bool isPressed = false;

    public GameObject scanText;
    public GameObject mineText;

    public void ToggleMode()
    {
        if(isPressed)
        {
            TileManager.Instance.mode = MineMode.Scan;

            scanText.SetActive(true);
            mineText.SetActive(false);
        }
        else
        {
            TileManager.Instance.mode = MineMode.Extract;

            scanText.SetActive(false);
            mineText.SetActive(true);
        }

        isPressed = !isPressed;
    }
}

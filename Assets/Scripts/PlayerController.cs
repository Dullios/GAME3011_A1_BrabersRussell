using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rigidBody;

    [SerializeField]
    private GameState gameState;

    public GameObject exclamation;
    public float speed;

    [SerializeField]
    private bool spotDetected;
    private SpotData currentSpot;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        gameState = GameState.MainGame;
    }

    // Update is called once per frame
    void Update()
    {
        switch(gameState)
        {
            case GameState.MainGame:
                Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), transform.position.y, Input.GetAxis("Vertical"));

                movement *= speed;
                rigidBody.velocity = movement;

                if(spotDetected)
                {
                    if(Input.GetKeyDown(KeyCode.Space))
                    {
                        TileManager.Instance.gameObject.SetActive(true);
                        TileManager.Instance.StartMiniGame(currentSpot);

                        gameState = GameState.MiniGame;
                    }
                }
                break;
            
            case GameState.MiniGame:
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    TileManager.Instance.gameObject.SetActive(false);

                    gameState = GameState.MainGame;
                }
                break;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        exclamation.SetActive(true);
        spotDetected = true;
        currentSpot = other.GetComponent<SpotData>();
    }

    private void OnTriggerExit(Collider other)
    {
        exclamation.SetActive(false);
        spotDetected = false;
    }
}

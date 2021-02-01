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
                        bool toggle = TileManager.Instance.gameObject.activeSelf ? false : true;
                        TileManager.Instance.gameObject.SetActive(toggle);
                    }
                }
                break;
            
            case GameState.MiniGame:

                break;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        exclamation.SetActive(true);
        spotDetected = true;
    }

    private void OnTriggerExit(Collider other)
    {
        exclamation.SetActive(false);
        spotDetected = false;
    }
}

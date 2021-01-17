using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rigidBody;

    [SerializeField]
    private GameState gameState;
    public float speed;

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
                break;
            
            case GameState.MiniGame:

                break;
        }
    }
}

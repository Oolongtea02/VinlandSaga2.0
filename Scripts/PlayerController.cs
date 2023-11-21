using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 6;
    [SerializeField] private float jumpForce = 50f;
    [SerializeField] public int health;
    private Vector2 moveDirection;
    Rigidbody2D playerRB;
    RaycastHit2D hit;
    RaycastHit2D enemyhit;
    enum Direction
    {
        left,
        right
    }
    enum playerState
    {
        neutral,
        blocking
    }
    Direction playerDirection;
    playerState thePlayerState;
    void Start()
    {
        health = 3;
        playerRB = gameObject.GetComponent<Rigidbody2D>();
        playerDirection = Direction.right;
        thePlayerState = playerState.neutral;
    }

    void OnMove(InputValue movementValue)
    {
        moveDirection = movementValue.Get<Vector2>();
    }
    void OnJump(InputValue movementValue)
    {
        hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), new Vector2(0, -1), Mathf.Infinity);
        // Check if player is grounded
        if (hit.distance < 1.0f)
        {
            //playerRB.AddForce(new Vector3(0.0f, 10.0f, 0.0f), ForceMode2D.Impulse);
            playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce);
        }
        //Debug.DrawRay(new Vector2(transform.position.x, transform.position.y), Vector3.right, Color.green, 5.0f);
    }
    void OnFire(InputValue fireValue)
    {
        float directionNum;
        // Player facing left
        if (playerDirection == Direction.left)
        {
            directionNum = -1;
        }
        // Player facing right
        else
        {
            directionNum = 1;
        }
        enemyhit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), new Vector2(directionNum, 0), Mathf.Infinity);
        if ((enemyhit.distance < 3.0f) && (enemyhit.collider != null))
        {
            if (enemyhit.rigidbody.tag == "Killable") // check if object hit was enemy
            {
                Destroy(enemyhit.rigidbody.gameObject);
                Debug.Log("Enemy Killed");
            }
        }
    }
    private void FixedUpdate()
    {
        playerRB.velocity = new Vector2(moveDirection.x * movementSpeed, playerRB.velocity.y);
        //Debug.Log(playerDirection);
        if (playerRB.velocity.x < 0)
        {
            playerDirection = Direction.left;
        }
        else if (playerRB.velocity.x > 0)
        {
            playerDirection = Direction.right;
        }
        if (Input.GetKey(KeyCode.Mouse1))
        {
            thePlayerState = playerState.blocking;
        }
        else
        {
            thePlayerState = playerState.neutral;
        }
    }
    public void DamagePlayer()
    {
        // If player isn't blocking, deduct health
        if (thePlayerState != playerState.blocking)
        {
            health--;
            Debug.Log("Health: " + health);
        }
        else
        {
            Debug.Log("Damage Blocked");
        }
    }
}

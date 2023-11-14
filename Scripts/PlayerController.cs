using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    Rigidbody2D playerRB;
    private float Move;
    public Vector2 boxSize;
    public float castDistance;
    [SerializeField] private LayerMask groundLayer;

    RaycastHit2D enemyhit;

    void Start()
    {
        playerRB = gameObject.GetComponent<Rigidbody2D>();
    }

    void OnMove(InputValue movementValue)
    {
        // playerRB.velocity = new Vector2(movementValue.Get<Vector2>().x * movementSpeed, playerRB.velocity.y);
    }

    void OnJump(InputValue movementValue)
    {
        // Only jump if player is grounded
        if (IsGrounded())
        {
            Debug.Log("Jump");
            playerRB.AddForce(new Vector2(playerRB.velocity.x, 500.0f));
        }
    }

    public bool IsGrounded()
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
    //}

    void OnFire(InputValue fireValue)
    {
        enemyhit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), new Vector2(1, 0), Mathf.Infinity);
        if (enemyhit.distance < 3.0f)
        {
            if (enemyhit.rigidbody != null && enemyhit.rigidbody.CompareTag("Killable"))
            {
                Destroy(enemyhit.rigidbody.gameObject);
            }
        }
    }

    private void Update()
    {
        Move = Input.GetAxis("Horizontal");
        playerRB.velocity = new Vector2(Move * movementSpeed, playerRB.velocity.y);
    }
}

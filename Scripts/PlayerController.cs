using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 8;
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] public int health;
    public Animator playerAnim;
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

    private SpriteRenderer spriteRenderer;
    Direction playerDirection;
    playerState thePlayerState;
    [SerializeField] public bool hasWeapon;
    public ParticleSystem walkParticles;
    public ParticleSystem jumpParticles;
    public AudioSource playerAudio;
    public AudioClip jump;
    public AudioClip atk;
    public AudioClip blk;
    void Start()
    {
        health = 3;
        playerRB = gameObject.GetComponent<Rigidbody2D>();
        playerDirection = Direction.right;
        thePlayerState = playerState.neutral;
        hasWeapon = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerAudio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (playerDirection == Direction.left)
        {
            spriteRenderer.flipX = true;
        }
        else if (playerDirection == Direction.right)
        {
            spriteRenderer.flipX = false;
        }
    }

    void OnMove(InputValue movementValue) 
    {
        moveDirection = movementValue.Get<Vector2>();
        if ((Input.GetKeyDown("a") || Input.GetKeyDown("d")))
        {
            playerAnim.SetBool("isWalking", true);
            walkParticles.Play();
        }
        else
        {
            // Stop walk animation if not moving
            playerAnim.SetBool("isWalking", false);
            walkParticles.Stop();
        }
    }
    void OnJump(InputValue movementValue)
    {
        hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), new Vector2(0, -1), Mathf.Infinity);
        // Check if player is grounded
        if ((hit.distance < 1.0f) && (hit.collider != null))
        {
            playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce);
            playerAnim.Play("Jump");
            jumpParticles.Play();
            playerAudio.clip = jump;
            playerAudio.Play();
        }
    
    }
    void OnFire(InputValue fireValue)
    {
        if (hasWeapon){
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
            playerAnim.Play("Attack");
            playerAudio.clip = atk;
            playerAudio.Play();
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
            playerAudio.clip = blk;
            playerAudio.Play();
            playerAnim.Play("Block");
        }
        else
        {
            thePlayerState = playerState.neutral;
            playerAnim.StopPlayback();
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

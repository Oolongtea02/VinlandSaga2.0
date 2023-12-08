using UnityEngine;
using System.Collections;

public enum EnemyState
{
    Patrol,
    Charge
}
public class EnemyBehavior : MonoBehaviour
{
    public EnemyState currentState = EnemyState.Patrol;
    [SerializeField] public float moveSpeed = 2f;
    [SerializeField] public float chargeSpeed = 2f;
    [SerializeField] public float patrolDist = 5f;
    [SerializeField] public float lookDistance = 7.5f;
    public int health;
    private Rigidbody2D rb;
    private Renderer enemyRenderer;
    private SpriteRenderer spriteRenderer;
    public Sprite patrol;
    public Sprite angry;
    public Animator enemyAnim;
    private Vector3 leftPoint;
    private Vector3 rightPoint;
    private Vector3 targetPoint;
    private bool isColliding = false;

    private bool playerInSight = false;
    private Transform playerTransform;

    void Start()
    {
        leftPoint = transform.position - new Vector3(patrolDist, 0f, 0f);
        rightPoint = transform.position + new Vector3(patrolDist, 0f, 0f);
        targetPoint = rightPoint;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        enemyRenderer = GetComponent<Renderer>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        health = 100;
    }

    void Update()
    {
        switch (currentState)
        {
            case EnemyState.Patrol:
                Patrol();
                break;
            case EnemyState.Charge:
                Charge();
                break;
        }
    }

    void Patrol()
    {
        if (!playerInSight)
        {
            enemyAnim.SetBool("isAngry", false);
            Vector2 point = targetPoint - transform.position;

            if (targetPoint == rightPoint)
            {
                rb.velocity = new Vector2(moveSpeed, 0);
            }
            else
            {
                rb.velocity = new Vector2(-moveSpeed, 0);
            }

            if (Vector2.Distance(transform.position, targetPoint) < 0.5f && targetPoint == rightPoint)
            {
                targetPoint = leftPoint;
                
            }
            if (Vector2.Distance(transform.position, targetPoint) < 0.5f && targetPoint == leftPoint)
            {
                targetPoint = rightPoint;
            }
        }
        else
        {
            currentState = EnemyState.Charge;
        }
    }

    void Charge()
    {
        enemyAnim.SetBool("isAngry", true);
        Vector2 direction = (playerTransform.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * chargeSpeed, 0);

        if (!playerInSight)
        {
            currentState = EnemyState.Patrol;
            enemyRenderer.material.color = Color.white;
        }
        else
        {
            enemyRenderer.material.color = Color.red;
        }
    }

    private IEnumerator DamagePlayerRepeatedly(PlayerController playerController)
    {
        while (isColliding)
        {
            Debug.Log("Attacking");
            enemyAnim.Play("attack");
            playerController.DamagePlayer();
            yield return new WaitForSeconds(2.0f);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            health -= 10;
            isColliding = true;
            StartCoroutine(DamagePlayerRepeatedly(other.gameObject.GetComponent<PlayerController>()));
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("out of range");
            isColliding = false;
        }
    }

    void FixedUpdate()
    {
        if (rb.velocity.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (rb.velocity.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        // Check if player is in LOS
        RaycastHit2D hit = Physics2D.Raycast(transform.position, playerTransform.position - transform.position, lookDistance);
        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            playerInSight = true;
        }
        else
        {
            playerInSight = false;
        }
    }
}

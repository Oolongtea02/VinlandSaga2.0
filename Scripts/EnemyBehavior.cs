using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] public float lookDistance = 10f;
    private Rigidbody2D rb;
    private Renderer enemyRenderer;
    private Vector3 leftPoint;
    private Vector3 rightPoint;
    private Vector3 targetPoint;

    private bool playerInSight = false;
    private Transform playerTransform;


    void Start()
    {
        leftPoint = transform.position - new Vector3(patrolDist, 0f, 0f);
        rightPoint = transform.position + new Vector3(patrolDist, 0f, 0f);
        targetPoint = rightPoint;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        enemyRenderer = GetComponent<Renderer>();
        rb = GetComponent<Rigidbody2D>();
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

    // Check collision with player
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().DamagePlayer();
        }
    }

    void FixedUpdate()
    {
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

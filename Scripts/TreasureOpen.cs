using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureOpen : MonoBehaviour
{
    public Animator chestAnim;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            chestAnim.Play("open");
            GameManager.GameOver();
        }
    }
}

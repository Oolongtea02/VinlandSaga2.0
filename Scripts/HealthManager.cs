using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    public Image healthBar;
    public float healthAmount = 100f;
    public EnemyBehavior pH;
    public static bool gameOver = false;

    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("This is the player's health");
        Debug.Log(pH.health);
        //healthBar.fillAmount = pH.health / 100;
            

        if(pH != null)
        {
            TakeDamage(10);
        }
    }

    

    // if(!PauseMenu.isPaused)
    /* {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            //TakeDamage(20);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Heal(10);
        }
    } 
} */

    public void TakeDamage(float damage)
    {
        healthAmount -= damage;
        healthBar.fillAmount = healthAmount / 100f;
        if (healthAmount <= 0)
        {
            gameOver = true;
            Debug.Log("Game Over!");
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }


    /* public void Heal(float healingAmount)
    {
        healthAmount += healingAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);

        healthBar.fillAmount = healthAmount / 100f;
    } */
}
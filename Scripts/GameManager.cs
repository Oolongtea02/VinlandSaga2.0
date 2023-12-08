using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool gameOver = false;

    public static void GameOver()
    {
        gameOver = true;
        Debug.Log("Game Over!");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    public bool gameOver = false;

    public void GameOver()
    {
        gameOver = true;
        Debug.Log("Game Over!");
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("Chaselevel");
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("UI");
    }
}

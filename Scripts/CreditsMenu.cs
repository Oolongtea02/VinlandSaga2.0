using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.UI;

public class CreditsMenu : MonoBehaviour
{
    public void ReturntoStart()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("UI");
        }
    
    }
}

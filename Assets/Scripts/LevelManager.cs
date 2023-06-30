using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    /// <summary>
    /// Navigates to first scene
    /// </summary>
    public void FirstScene()
    {
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Navigate to next scene from current one
    /// </summary>
    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

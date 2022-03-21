using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Launch3x3Map()
    {
        SceneManager.LoadScene("3x3");
    }

    public void Launch3x4Map()
    {
        SceneManager.LoadScene("ScaledMapforJoseph");
    }

    public void LaunchMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LaunchMaps()
    {
        SceneManager.LoadScene("ScaledMapForJoseph");
    }

    public void Options()
    {
        // For options
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void CharacterSelect()
    {
        // For character selection
    }

    public void Remach()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

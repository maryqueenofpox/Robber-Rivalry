using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManagement : MonoBehaviour
{
    [SerializeField] int mainMenuIndex;
    [SerializeField] int smallMapIndex;
    [SerializeField] int originalMapIndex;
    [SerializeField] int mapsIndex;
    [SerializeField] int optionsIndex;
    [SerializeField] int characterSelectIndex;
    [SerializeField] int controlsScreenIndex;

    public void Launch3x3Map()
    {
        SceneManager.LoadScene(smallMapIndex);
    }

    public void Launch3x4Map()
    {
        SceneManager.LoadScene(originalMapIndex);
    }

    public void LaunchMainMenu()
    {
        SceneManager.LoadScene(mainMenuIndex);
    }

    public void MapsSelect()
    {
        SceneManager.LoadScene(mapsIndex);
    }

    public void Controls()
    {
        SceneManager.LoadScene(controlsScreenIndex);
    }

    public void Options()
    {
        SceneManager.LoadScene(optionsIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void CharacterSelect()
    {
        SceneManager.LoadScene(characterSelectIndex);
    }

    public void Remach()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

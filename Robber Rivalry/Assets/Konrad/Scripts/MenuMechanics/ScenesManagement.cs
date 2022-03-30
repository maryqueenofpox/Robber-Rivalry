using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManagement : MonoBehaviour
{
    [SerializeField] ScenesStuff scenesStuff;

    public void Launch3x3Map()
    {
        SceneManager.LoadScene(scenesStuff.smallMapIndex);
    }

    public void Launch3x4Map()
    {
        SceneManager.LoadScene(scenesStuff.originalMapIndex);
    }

    public void LaunchMainMenu()
    {
        SceneManager.LoadScene(scenesStuff.mainMenuIndex);
    }

    public void MapsSelect()
    {
        SceneManager.LoadScene(scenesStuff.mapsIndex);
    }

    public void Controls()
    {
        SceneManager.LoadScene(scenesStuff.controlsScreenIndex);
    }

    public void Options()
    {
        SceneManager.LoadScene(scenesStuff.optionsIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void CharacterSelect()
    {
        SceneManager.LoadScene(scenesStuff.characterSelectIndex);
    }

    public void Remach()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void TutorialScreen()
    {
        SceneManager.LoadScene(scenesStuff.tutorialScreenIndex);
    }
}

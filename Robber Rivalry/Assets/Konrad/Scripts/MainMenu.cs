using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //[SerializeField] Scene scene3x3;
    //[SerializeField] Scene scene3x4;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Launch3x3Map()
    {
        SceneManager.LoadScene("3x3Map");
    }

    public void Launch3x4Map()
    {
        SceneManager.LoadScene("ScaledMapforJoseph");
    }

    public void LaunchMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

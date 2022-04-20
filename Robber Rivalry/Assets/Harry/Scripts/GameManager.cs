using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool counDownDone = false;
    // Start is called before the first frame update
    void Start()
    {
        counDownDone = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTimeScale()
    {
        if (counDownDone == false)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}

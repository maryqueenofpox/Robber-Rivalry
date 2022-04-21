using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool counDownDone = false;
    [SerializeField] GameObject countDownTimer;
    float quickTimer = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        counDownDone = false;
        countDownTimer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        quickTimer -= Time.fixedUnscaledDeltaTime;
        if (quickTimer < 0f)
            countDownTimer.SetActive(true);
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
            GetComponent<GameManager>().enabled = false;
        }
    }
}

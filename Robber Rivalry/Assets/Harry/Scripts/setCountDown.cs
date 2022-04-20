using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setCountDown : MonoBehaviour
{
    private GameManager gm;
    private Animator anim;

    private void Start()
    {
        gm = GameObject.Find("EventSystem").GetComponent<GameManager>();

        gameObject.SetActive(true);
        anim = gameObject.GetComponent<Animator>();
        anim.Play("Image", -1, 0f);

        gm.SetTimeScale();
    }

    public void SetCountDown()
    {
        gm.counDownDone = true;
        gm.SetTimeScale();

        gameObject.SetActive(false);
    }
}

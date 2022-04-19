using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setCountDown : MonoBehaviour
{
    private GameManager gm;
    private Animator anim;

    private void Start()
    {
        gameObject.SetActive(true);
        anim = gameObject.GetComponent<Animator>();
        anim.Play("Image", -1, 0f);

    }

    public void SetCountDown()
    {
        gm = GameObject.Find("EventSystem").GetComponent<GameManager>();

        gm.counDownDone = true;

        gameObject.SetActive(false);
    }
}

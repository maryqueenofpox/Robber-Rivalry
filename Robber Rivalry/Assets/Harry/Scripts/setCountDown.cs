using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setCountDown : MonoBehaviour
{
    private GameManager gm;
    private Animator anim;
    [SerializeField] float audioTimerPlayDelay = 1f;
    [SerializeField] AudioSource audioSource;

    private void Start()
    {
        gm = GameObject.Find("EventSystem").GetComponent<GameManager>();

        gameObject.SetActive(true);
        anim = gameObject.GetComponent<Animator>();
        StartCoroutine(PlayAudioForCountDown());
        anim.Play("Image", -1, 0f);

        gm.SetTimeScale();
    }

    public void SetCountDown()
    {
        gm.counDownDone = true;
        gm.SetTimeScale();

        gameObject.SetActive(false);
    }

    IEnumerator PlayAudioForCountDown()
    {
        audioSource.Play();

        yield return new WaitForSeconds(audioTimerPlayDelay);

        audioSource.Play();

        yield return new WaitForSeconds(audioTimerPlayDelay);

        audioSource.Play();

        yield return new WaitForSeconds(audioTimerPlayDelay);

        audioSource.Play();

        yield return new WaitForSeconds(audioTimerPlayDelay);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    [SerializeField] Timer timer;
    [SerializeField] float movePlatformTimer;
    Vector3 originalPosition;
    [SerializeField] GameObject escapeWall;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        escapeWall.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(timer.timer <= movePlatformTimer && timer.timer > 0)
        {
            escapeWall.SetActive(false);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(30.12f, 0f, -0.300f), 10f * Time.deltaTime);
        }

        if (timer.timer <= 0)
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, 5f * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            other.transform.parent = transform;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            other.transform.parent = null;
    }
}

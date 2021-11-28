using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndGame : MonoBehaviour
{
    [SerializeField] Timer timer;
    [SerializeField] float movePlatformTimer;
    Vector3 originalPosition;
    [SerializeField] GameObject escapeWall;

    [SerializeField] GameObject endGamePanel;
    [SerializeField] GameObject player_1_Crown;
    [SerializeField] GameObject player_2_Crown;
    [SerializeField] GameObject player_3_Crown;
    [SerializeField] GameObject player_4_Crown;

    /*
    [SerializeField] TextMeshProUGUI player_1_Score;
    [SerializeField] TextMeshProUGUI player_2_Score;
    [SerializeField] TextMeshProUGUI player_3_Score;
    [SerializeField] TextMeshProUGUI player_4_Score;
    */

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        escapeWall.SetActive(true);

        endGamePanel.SetActive(false);
        player_1_Crown.SetActive(false);
        player_2_Crown.SetActive(false);
        player_3_Crown.SetActive(false);
        player_4_Crown.SetActive(false);
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

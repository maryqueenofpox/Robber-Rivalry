using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGame : MonoBehaviour
{
    [SerializeField] Timer timer;
    [SerializeField] float movePlatformTimer;
    Vector3 originalPosition;
    [SerializeField] GameObject escapeWall;
    [SerializeField] GameObject endGamePanel;
    [SerializeField] float gemRewardAmount;

    [Header("Crown Objects")]
    [SerializeField] GameObject player_1_Crown;
    [SerializeField] GameObject player_2_Crown;
    [SerializeField] GameObject player_3_Crown;
    [SerializeField] GameObject player_4_Crown;

    [Header("Player Score")]
    [SerializeField] TextMeshProUGUI player_1_Score;
    [SerializeField] TextMeshProUGUI player_2_Score;
    [SerializeField] TextMeshProUGUI player_3_Score;
    [SerializeField] TextMeshProUGUI player_4_Score;

    [Header("Player Game Objects")]
    [SerializeField] GameObject Player1;
    [SerializeField] GameObject Player2;
    [SerializeField] GameObject Player3;
    [SerializeField] GameObject Player4;

    [Header("LootGrabber Script")]
    [SerializeField] LootGrabber player_1_Script;
    [SerializeField] LootGrabber player_2_Script;
    [SerializeField] LootGrabber player_3_Script;
    [SerializeField] LootGrabber player_4_Script;

    [Header("Eliminated X Symbol")]
    [SerializeField] TextMeshProUGUI x1;
    [SerializeField] TextMeshProUGUI x2;
    [SerializeField] TextMeshProUGUI x3;
    [SerializeField] TextMeshProUGUI x4;

    float max;
    bool doOnce;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        originalPosition = transform.position;
        escapeWall.SetActive(true);

        endGamePanel.SetActive(false);
        player_1_Crown.SetActive(false);
        player_2_Crown.SetActive(false);
        player_3_Crown.SetActive(false);
        player_4_Crown.SetActive(false);

        x1.enabled = false;
        x2.enabled = false;
        x3.enabled = false;
        x4.enabled = false;

        doOnce = true;
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
        {
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, 5f * Time.deltaTime);
            escapeWall.SetActive(true);
            endGamePanel.SetActive(true);
            max = Mathf.Max(player_1_Script.loot, player_2_Script.loot, player_3_Script.loot, player_4_Script.loot);
            Scoring();
            Time.timeScale = 0f;
        }
    }

    void Scoring()
    {
        if (doOnce)
        {
            EnableCrown();
            doOnce = false;
        }

        player_1_Score.text = player_1_Script.loot.ToString();
        player_2_Score.text = player_2_Script.loot.ToString();
        player_3_Score.text = player_3_Script.loot.ToString();
        player_4_Score.text = player_4_Script.loot.ToString();

    }

    void EnableCrown()
    {
        if (Player1.transform.parent == transform)
        {
            if (player_1_Script.loot == max)
                player_1_Crown.SetActive(true);

            if (Player1.transform.childCount > 0)
                player_1_Script.loot += gemRewardAmount;
        }
        else
        {
            x1.enabled = true;
            player_1_Script.loot = 0;
        }

        if (Player2.transform.parent == transform)
        {
            if (player_2_Script.loot == max)
                player_2_Crown.SetActive(true);

            if (Player2.transform.childCount > 0)
                player_2_Script.loot += gemRewardAmount;
        }
        else
        {
            x2.enabled = true;
            player_2_Script.loot = 0;
        }

        if (Player3.transform.parent == transform)
        {
            if (player_3_Script.loot == max)
                player_3_Crown.SetActive(true);

            if (Player3.transform.childCount > 0)
                player_3_Script.loot += gemRewardAmount;
        }
        else
        {
            x3.enabled = true;
            player_3_Script.loot = 0;
        }

        if (Player4.transform.parent == transform)
        {
            if (player_4_Script.loot == max)
                player_4_Crown.SetActive(true);

            if (Player4.transform.childCount > 0)
                player_4_Script.loot += gemRewardAmount;
        }
        else
        {
            x4.enabled = true;
            player_4_Script.loot = 0;
        }
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

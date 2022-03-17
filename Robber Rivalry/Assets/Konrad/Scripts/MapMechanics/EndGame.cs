using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    [SerializeField] EndGamePlatformFall endGamePlatformFallScript;
    [SerializeField] Timer timer;
    [SerializeField] GameObject endGamePanel;
    [SerializeField] float gemRewardAmount;
    [SerializeField] Image fuse;
    public float fuseTimer = 20f;
    [SerializeField] float penalty = 5f;

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

    float max;
    bool doOnce;
    bool doGemAddOnce;


    // Start is called before the first frame update
    void Start()
    {
        endGamePanel.SetActive(false);
        player_1_Crown.SetActive(false);
        player_2_Crown.SetActive(false);
        player_3_Crown.SetActive(false);
        player_4_Crown.SetActive(false);

        doOnce = true;
        doGemAddOnce = true;
        fuse.enabled = false;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (timer.timer <= 0)
        {
            endGamePanel.SetActive(true);
            AddGemPoints();
            max = Mathf.Max(player_1_Script.loot, player_2_Script.loot, player_3_Script.loot, player_4_Script.loot);
            Scoring();
            Time.timeScale = 0f;
        }

        if (timer.timer <= fuseTimer)
            fuse.enabled = true;

        if (fuse.enabled == true)
        {
            fuse.fillAmount = timer.timer / fuseTimer;
        }
    }

    void AddGemPoints()
    {
        if (doGemAddOnce)
        {
            if (Player1.transform.parent == transform)
            {
                if (Player1.transform.Find("Gem"))
                {
                    player_1_Script.loot += gemRewardAmount;
                    player_1_Script.loot.ToString();
                }
            }
            else
            {
                player_1_Script.loot -= penalty;
                player_1_Script.loot.ToString();
            }

            if (Player2.transform.parent == transform)
            {
                if (Player2.transform.Find("Gem"))
                {
                    player_2_Script.loot += gemRewardAmount;
                    player_1_Script.loot.ToString();
                }
            }
            else
            {
                player_2_Script.loot -= penalty;
                player_2_Script.loot.ToString();
            }

            if (Player3.transform.parent == transform)
            {
                if (Player3.transform.Find("Gem"))
                {
                    player_3_Script.loot += gemRewardAmount;
                    player_1_Script.loot.ToString();
                }
            }
            else
            {
                player_3_Script.loot -= penalty;
                player_3_Script.loot.ToString();
            }

            if (Player4.transform.parent == transform)
            {
                if (Player4.transform.Find("Gem"))
                {
                    player_4_Script.loot += gemRewardAmount;
                    player_1_Script.loot.ToString();
                }
            }
            else
            {
                player_4_Script.loot -= penalty;
                player_4_Script.loot.ToString();
            }

            doGemAddOnce = false;
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
        if (player_1_Script.loot == max)
                player_1_Crown.SetActive(true);

        if (player_2_Script.loot == max)
                player_2_Crown.SetActive(true);

        if (player_3_Script.loot == max)
                player_3_Crown.SetActive(true);

        if (player_4_Script.loot == max)
                player_4_Crown.SetActive(true);
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

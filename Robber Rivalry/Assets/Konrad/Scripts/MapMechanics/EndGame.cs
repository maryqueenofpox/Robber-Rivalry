using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class EndGame : MonoBehaviour
{
    [SerializeField] EndGamePlatformFall endGamePlatformFallScript;
    [SerializeField] Timer timer;
    [SerializeField] GameObject endGamePanel;
    [SerializeField] float gemRewardAmount;
    public Image fuse;
    public float fuseTimer = 20f;
    

    [SerializeField] AudioSource fuseSound;
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

    [Header("Player 2nd Icon")]
    [SerializeField] GameObject Player_1_Second;
    [SerializeField] GameObject Player_2_Second;
    [SerializeField] GameObject Player_3_Second;
    [SerializeField] GameObject Player_4_Second;

    [Header("Player 3rd Icon")]
    [SerializeField] GameObject Player_1_Third;
    [SerializeField] GameObject Player_2_Third;
    [SerializeField] GameObject Player_3_Third;
    [SerializeField] GameObject Player_4_Third;

    [Header("Player 4th Icon")]
    [SerializeField] GameObject Player_1_Fourth;
    [SerializeField] GameObject Player_2_Fourth;
    [SerializeField] GameObject Player_3_Fourth;
    [SerializeField] GameObject Player_4_Fourth;

    [Header("Player Shared Crown Icons")]
    [SerializeField] GameObject Player_1_Shared_Crown;
    [SerializeField] GameObject Player_2_Shared_Crown;
    [SerializeField] GameObject Player_3_Shared_Crown;
    [SerializeField] GameObject Player_4_Shared_Crown;

    [Header("Player Shared Second Icons")]
    [SerializeField] GameObject Player_1_Shared_Second;
    [SerializeField] GameObject Player_2_Shared_Second;
    [SerializeField] GameObject Player_3_Shared_Second;
    [SerializeField] GameObject Player_4_Shared_Second;

    [Header("Player Shared Third Icons")]
    [SerializeField] GameObject Player_1_Shared_Third;
    [SerializeField] GameObject Player_2_Shared_Third;
    [SerializeField] GameObject Player_3_Shared_Third;
    [SerializeField] GameObject Player_4_Shared_Third;

    [Header("Player Shared Fourth Icons")]
    [SerializeField] GameObject Player_1_Shared_Fourth;
    [SerializeField] GameObject Player_2_Shared_Fourth;
    [SerializeField] GameObject Player_3_Shared_Fourth;
    [SerializeField] GameObject Player_4_Shared_Fourth;

    float max;
    bool doOnce;
    bool doGemAddOnce;
    bool playOnce;

    [Header("Arrays")]
    float[] ranking = new float[4];
    GameObject[] Player1Rankings = new GameObject[4];
    GameObject[] Player2Rankings = new GameObject[4];
    GameObject[] Player3Rankings = new GameObject[4];
    GameObject[] Player4Rankings = new GameObject[4];
    TextMeshProUGUI[] ScoreRanking = new TextMeshProUGUI[4];
    GameObject[] Player1SharedSprites = new GameObject[4];
    GameObject[] Player2SharedSprites = new GameObject[4];
    GameObject[] Player3SharedSprites = new GameObject[4];
    GameObject[] Player4SharedSprites = new GameObject[4];

    // Start is called before the first frame update
    void Start()
    {
        endGamePanel.SetActive(false);

        player_1_Crown.SetActive(false);
        player_2_Crown.SetActive(false);
        player_3_Crown.SetActive(false);
        player_4_Crown.SetActive(false);

        Player_1_Shared_Crown.SetActive(false);
        Player_2_Shared_Crown.SetActive(false);
        Player_3_Shared_Crown.SetActive(false);
        Player_4_Shared_Crown.SetActive(false);

        Player_1_Second.SetActive(false);
        Player_2_Second.SetActive(false);
        Player_3_Second.SetActive(false);
        Player_4_Second.SetActive(false);

        Player_1_Shared_Second.SetActive(false);
        Player_2_Shared_Second.SetActive(false);
        Player_3_Shared_Second.SetActive(false);
        Player_4_Shared_Second.SetActive(false);

        Player_1_Third.SetActive(false);
        Player_2_Third.SetActive(false);
        Player_3_Third.SetActive(false);
        Player_4_Third.SetActive(false);

        Player_1_Shared_Third.SetActive(false);
        Player_2_Shared_Third.SetActive(false);
        Player_3_Shared_Third.SetActive(false);
        Player_4_Shared_Third.SetActive(false);

        Player_1_Fourth.SetActive(false);
        Player_2_Fourth.SetActive(false);
        Player_3_Fourth.SetActive(false);
        Player_4_Fourth.SetActive(false);

        Player_1_Shared_Fourth.SetActive(false);
        Player_2_Shared_Fourth.SetActive(false);
        Player_3_Shared_Fourth.SetActive(false);
        Player_4_Shared_Fourth.SetActive(false);



        playOnce = false;
        doOnce = true;
        doGemAddOnce = true;
        fuse.enabled = false;
        Time.timeScale = 1f;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (timer.timer <= 0)
        {
            fuseSound.Stop();
            endGamePanel.SetActive(true);

            //EventSystem eventSystem = EventSystem.current;
            //eventSystem.SetSelectedGameObject(firstButtonToBeSelected);

            AddGemPoints();
            max = Mathf.Max(player_1_Script.loot, player_2_Script.loot, player_3_Script.loot, player_4_Script.loot);

            //Assigns player scores to an array, and sorts them lowest (0) to highest (3)
            ranking[0] = player_1_Script.loot;
            ranking[1] = player_2_Script.loot;
            ranking[2] = player_3_Script.loot;
            ranking[3] = player_4_Script.loot;
            Array.Sort(ranking);

            EnableCrown();

            //Assigns player score textmeshes to ScoreRanking array
            ScoreRanking[0] = player_4_Score;
            ScoreRanking[1] = player_3_Score;
            ScoreRanking[2] = player_2_Score;
            ScoreRanking[3] = player_1_Score;

            //For loop that lasts until i is one less than array.length
            for (int i = 0; i < ScoreRanking.Length; i++)
            {
                //assigns the scores into their respective textmeshes (1st place score in 1st place text mesh)
                ScoreRanking[i].text = ranking[i].ToString();
            }

            Scoring();
            if (player_1_Script.loot < 0)
            {
                player_1_Script.loot = 0;
                player_1_Score.text = player_1_Script.loot.ToString();
            }

            if (player_2_Script.loot < 0)
            {
                player_2_Script.loot = 0;
                player_2_Score.text = player_2_Script.loot.ToString();
            }

            if (player_3_Script.loot < 0)
            {
                player_3_Script.loot = 0;
                player_3_Score.text = player_3_Script.loot.ToString();
            }

            if (player_4_Script.loot < 0)
            {
                player_4_Script.loot = 0;
                player_4_Score.text = player_4_Script.loot.ToString();
            }
            Time.timeScale = 0f;
        }

        if (timer.timer <= fuseTimer)
        {
            fuse.enabled = true;
            if (!playOnce)
            {
                fuseSound.Play();
                playOnce = true;
            }
        }
         

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

        // Should be safe to remove these since theyre now captured in the new loop. Could move that here aswell and should work. Enabling this breaks the loop stuff so pls dont ;-;

        // player_1_Score.text = player_1_Script.loot.ToString();
        // player_2_Score.text = player_2_Script.loot.ToString();
        // player_3_Score.text = player_3_Script.loot.ToString();
        // player_4_Score.text = player_4_Script.loot.ToString();

    }

    void EnableCrown()
    {
        // Assings player sprites to their respective Arrays
        Player1Rankings[0] = Player_1_Fourth;
        Player1Rankings[1] = Player_1_Third;
        Player1Rankings[2] = Player_1_Second;
        Player1Rankings[3] = player_1_Crown;

        Player2Rankings[0] = Player_2_Fourth;
        Player2Rankings[1] = Player_2_Third;
        Player2Rankings[2] = Player_2_Second;
        Player2Rankings[3] = player_2_Crown;

        Player3Rankings[0] = Player_3_Fourth;
        Player3Rankings[1] = Player_3_Third;
        Player3Rankings[2] = Player_3_Second;
        Player3Rankings[3] = player_3_Crown;

        Player4Rankings[0] = Player_4_Fourth;
        Player4Rankings[1] = Player_4_Third;
        Player4Rankings[2] = Player_4_Second;
        Player4Rankings[3] = player_4_Crown;

        // Assigns the shared sprites to their respective arrays
        Player1SharedSprites[0] = Player_1_Shared_Fourth;
        Player1SharedSprites[1] = Player_1_Shared_Third;
        Player1SharedSprites[2] = Player_1_Shared_Second;
        Player1SharedSprites[3] = Player_1_Shared_Crown;

        Player2SharedSprites[0] = Player_2_Shared_Fourth;
        Player2SharedSprites[1] = Player_2_Shared_Third;
        Player2SharedSprites[2] = Player_2_Shared_Second;
        Player2SharedSprites[3] = Player_2_Shared_Crown;

        Player3SharedSprites[0] = Player_3_Shared_Fourth;
        Player3SharedSprites[1] = Player_3_Shared_Third;
        Player3SharedSprites[2] = Player_3_Shared_Second;
        Player3SharedSprites[3] = Player_3_Shared_Crown;

        Player4SharedSprites[0] = Player_4_Shared_Fourth;
        Player4SharedSprites[1] = Player_4_Shared_Third;
        Player4SharedSprites[2] = Player_4_Shared_Second;
        Player4SharedSprites[3] = Player_4_Shared_Crown;

        // For loop that will run until i is one less than array length
        for (int i = 0; i < ranking.Length; i++)
        {
            // Checks for player 1's loot and compares to each ranked score value
            if (player_1_Script.loot == ranking[i] )
            {
                // Will enable the sprite that corresponds with the players point value (second place loss for second place points)
                Player1Rankings[i].SetActive(true);
            }
        }

        for (int i = 0; i < ranking.Length; i++)
        {
            if (player_2_Script.loot == ranking[i])
            {
                Player2Rankings[i].SetActive(true);
            }
        }

        for (int i = 0; i < ranking.Length; i++)
        {
            if (player_3_Script.loot == ranking[i])
            {
                Player3Rankings[i].SetActive(true);
            }
        }

        for (int i = 0; i < ranking.Length; i++)
        {
            if (player_4_Script.loot == ranking[i])
            {
                Player4Rankings[i].SetActive(true);
            }
        }

        //For Loop for player 1 shared sprite activation what runs until i = ranking.length
        for (int i = 0; i < ranking.Length; i++)
        {
            //Checks for player 1 loot and player 2 loot to be equal to the same ranking, ranking changes based on integer
            if (player_1_Script.loot == ranking[i] && player_2_Script.loot == ranking[i])
            {
                //if (Player1SharedSprites[i-1] && Player2SharedSprites[i-1])
                    //Activates the corresponding sprite for the related rank
                    Player1SharedSprites[i].SetActive(true);
                    Player2SharedSprites[i].SetActive(true);
                    Player1Rankings[i].SetActive(false);
                    Player2Rankings[i].SetActive(false);
                
            }

            //Checks for player 1 loot and player 3 loot to be equal to the same ranking
            if (player_1_Script.loot == ranking[i] && player_3_Script.loot == ranking[i])
            {
                Player1SharedSprites[i].SetActive(true);
                Player3SharedSprites[i].SetActive(true);
                Player1Rankings[i].SetActive(false);
                Player3Rankings[i].SetActive(false);
            }

            //Checks for player 1 loot and player 4 loot to be equal to the same ranking
            if (player_1_Script.loot == ranking[i] && player_4_Script.loot == ranking[i])
            {
                Player1SharedSprites[i].SetActive(true);
                Player4SharedSprites[i].SetActive(true);
                Player1Rankings[i].SetActive(false);
                Player4Rankings[i].SetActive(false);
            }
        }

        //For Loop for player 2 shared sprite activation what runs until i = ranking.length
        for (int i = 0; i < ranking.Length; i++)
        {
            if (player_2_Script.loot == ranking[i] && player_1_Script.loot == ranking[i])
            {
                Player2SharedSprites[i].SetActive(true);
                Player1SharedSprites[i].SetActive(true);
                Player2Rankings[i].SetActive(false);
                Player1Rankings[i].SetActive(false);
            }

            if (player_2_Script.loot == ranking[i] && player_3_Script.loot == ranking[i])
            {
                Player2SharedSprites[i].SetActive(true);
                Player3SharedSprites[i].SetActive(true);
                Player2Rankings[i].SetActive(false);
                Player3Rankings[i].SetActive(false);
            }

            if (player_2_Script.loot == ranking[i] && player_4_Script.loot == ranking[i])
            {
                Player2SharedSprites[i].SetActive(true);
                Player4SharedSprites[i].SetActive(true);
                Player2Rankings[i].SetActive(false);
                Player4Rankings[i].SetActive(false);
            }
        }

        //For Loop for player 3 shared sprite activation what runs until i = ranking.length
        for (int i = 0; i < ranking.Length; i++)
        {
            if (player_3_Script.loot == ranking[i] && player_1_Script.loot == ranking[i])
            {
                Player3SharedSprites[i].SetActive(true);
                Player1SharedSprites[i].SetActive(true);
                Player3Rankings[i].SetActive(false);
                Player1Rankings[i].SetActive(false);
            }

            if (player_3_Script.loot == ranking[i] && player_2_Script.loot == ranking[i])
            {
                Player3SharedSprites[i].SetActive(true);
                Player2SharedSprites[i].SetActive(true);
                Player3Rankings[i].SetActive(false);
                Player2Rankings[i].SetActive(false);
            }

            if (player_3_Script.loot == ranking[i] && player_4_Script.loot == ranking[i])
            {
                Player3SharedSprites[i].SetActive(true);
                Player4SharedSprites[i].SetActive(true);
                Player3Rankings[i].SetActive(false);
                Player4Rankings[i].SetActive(false);
            }
        }

        //Player 4 wouldnt need a for loop since they will be covered by all previous loops

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

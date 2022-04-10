using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
using System.Linq;

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

    [Header("AnimationWarning")]
    [SerializeField] GameObject animationWarning;

    float[] playerScores = new float[4];
    GameObject[] playerSpritesCrown = new GameObject[4];
    GameObject[] playerSpritesSecond = new GameObject[4];
    GameObject[] playerSpritesThird = new GameObject[4];
    GameObject[] playerSpritesFourth = new GameObject[4];

    Dictionary<string, float> doADictionaryPleaseRightAAAAAAA = new Dictionary<string, float>();
    bool oncePlease;
    System.Linq.IOrderedEnumerable<System.Collections.Generic.KeyValuePair<string, float>> sortedDict;
    GameObject[][] arrayOfArrays = new GameObject[4][];

    // Start is called before the first frame update
    void Start()
    {


        oncePlease = true;
        endGamePanel.SetActive(false);

        player_1_Crown.SetActive(false);
        player_2_Crown.SetActive(false);
        player_3_Crown.SetActive(false);
        player_4_Crown.SetActive(false);

        Player_1_Second.SetActive(false);
        Player_2_Second.SetActive(false);
        Player_3_Second.SetActive(false);
        Player_4_Second.SetActive(false);

        Player_1_Third.SetActive(false);
        Player_2_Third.SetActive(false);
        Player_3_Third.SetActive(false);
        Player_4_Third.SetActive(false);

        Player_1_Fourth.SetActive(false);
        Player_2_Fourth.SetActive(false);
        Player_3_Fourth.SetActive(false);
        Player_4_Fourth.SetActive(false);

     



        playOnce = false;
        doOnce = true;
        doGemAddOnce = true;
        fuse.enabled = false;
        Time.timeScale = 1f;

        arrayOfArrays[3] = playerSpritesCrown;
        arrayOfArrays[2] = playerSpritesSecond;
        arrayOfArrays[1] = playerSpritesThird;
        arrayOfArrays[0] = playerSpritesFourth;

        playerSpritesCrown[0] = player_1_Crown;
        playerSpritesCrown[1] = player_2_Crown;
        playerSpritesCrown[2] = player_3_Crown;
        playerSpritesCrown[3] = player_4_Crown;

        playerSpritesSecond[0] = Player_1_Second;
        playerSpritesSecond[1] = Player_2_Second;
        playerSpritesSecond[2] = Player_3_Second;
        playerSpritesSecond[3] = Player_4_Second;

        playerSpritesThird[0] = Player_1_Third;
        playerSpritesThird[1] = Player_2_Third;
        playerSpritesThird[2] = Player_3_Third;
        playerSpritesThird[3] = Player_4_Third;

        playerSpritesFourth[0] = Player_1_Fourth;
        playerSpritesFourth[1] = Player_2_Fourth;
        playerSpritesFourth[2] = Player_3_Fourth;
        playerSpritesFourth[3] = Player_4_Fourth;
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
            ranking[3] = player_1_Script.loot;
            ranking[2] = player_2_Script.loot;
            ranking[1] = player_3_Script.loot;
            ranking[0] = player_4_Script.loot;
            Array.Sort(ranking);

            playerScores[0] = player_1_Script.loot;
            playerScores[1] = player_2_Script.loot;
            playerScores[2] = player_3_Script.loot;
            playerScores[3] = player_4_Script.loot;

            if (oncePlease)
            {
                doADictionaryPleaseRightAAAAAAA.Add("Player1", player_1_Script.loot);
                doADictionaryPleaseRightAAAAAAA.Add("Player2", player_2_Script.loot);
                doADictionaryPleaseRightAAAAAAA.Add("Player3", player_3_Script.loot);
                doADictionaryPleaseRightAAAAAAA.Add("Player4", player_4_Script.loot);

                //doADictionaryPleaseRightAAAAAAA.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
                sortedDict = from entry in doADictionaryPleaseRightAAAAAAA orderby entry.Value descending select entry;

                oncePlease = false;
            }

            for (int i = 0; i < ranking.Length; i++)
            {
                string key = sortedDict.FirstOrDefault(x => x.Value == ranking[i]).Key;
                Debug.Log("Values bla: " + sortedDict.FirstOrDefault(x => x.Value == ranking[i]).Value);

                switch (key)
                {
                    case "Player1":
                        arrayOfArrays[i][0].SetActive(true);
                        doADictionaryPleaseRightAAAAAAA.Remove(key);
                        sortedDict = from entry in doADictionaryPleaseRightAAAAAAA orderby entry.Value descending select entry;
                        break;
                    case "Player2":
                        arrayOfArrays[i][1].SetActive(true);
                        doADictionaryPleaseRightAAAAAAA.Remove(key);
                        sortedDict = from entry in doADictionaryPleaseRightAAAAAAA orderby entry.Value descending select entry;
                        break;
                    case "Player3":
                        arrayOfArrays[i][2].SetActive(true);
                        doADictionaryPleaseRightAAAAAAA.Remove(key);
                        sortedDict = from entry in doADictionaryPleaseRightAAAAAAA orderby entry.Value descending select entry;
                        break;
                    case "Player4":
                        arrayOfArrays[i][3].SetActive(true);
                        doADictionaryPleaseRightAAAAAAA.Remove(key);
                        sortedDict = from entry in doADictionaryPleaseRightAAAAAAA orderby entry.Value descending select entry;
                        break;
                    default:
                        break;
                }
            }

            /*
            for (int i = 0; i < sortedDict.Count(); i++)
            {
                //var key = sortedDict.Where(pair => pair.Value == ranking[i]);
                var key = sortedDict.FirstOrDefault(x => x.Value == ranking[i]).Key;

                foreach (var item in key)
                {
                    Debug.Log("AAAAAAAAAAAAAAAAAAAAB: " + key);
                }

                switch (key)
                {
                    case "Player1":
                        Debug.Log("Playar1" + key);

                        if ()

                        break;
                    case "Player2":
                        Debug.Log("Playar2" + key);
                        break;
                    case "Player3":
                        Debug.Log("Playar3" + key);
                        break;
                    case "Player4":
                        Debug.Log("Playar4" + key);
                        break;
                    default:
                        break;
                }
            }
            */

            //EnableCrown();

            //Assigns player score textmeshes to ScoreRanking array
            ScoreRanking[0] = player_4_Score;
            ScoreRanking[1] = player_3_Score;
            ScoreRanking[2] = player_2_Score;
            ScoreRanking[3] = player_1_Score;

            /*
            for (int i = 0; i < ranking.Length; i++)
            {
                for (int jacob = 0; jacob < ranking.Length; jacob++)
                {
                    if (playerScores[jacob] == ranking[jacob])
                    {
                        if ()
                    }
                }
            }
            */

            //For loop that lasts until i is one less than array.length
            for (int i = 0; i < ScoreRanking.Length; i++)
            {
                //assigns the scores into their respective textmeshes (1st place score in 1st place text mesh)
                ScoreRanking[i].text = ranking[i].ToString();
            }

            //Scoring();

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

        if (timer.timer <= fuseTimer + 4)
        {
            animationWarning.SetActive(true);

            Destroy(animationWarning, 4);
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

      

        // For loop that will run until i is one less than array length
        for (int i = 0; i < ranking.Length; i++)
        {
            // Checks for player 1's loot and compares to each ranked score value
            if (player_1_Script.loot == ranking[i])
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

    }
    /*
     * 
    */
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
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

    [SerializeField] Button remachButton;
    [SerializeField] Button menuButton;


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

    

    bool doGemAddOnce;
    bool playOnce;

    [Header("Arrays")]
    float[] ranking = new float[4];
    TextMeshProUGUI[] ScoreRanking = new TextMeshProUGUI[4];

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

    float buttonDisableDuration = 1f;

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
        doGemAddOnce = true;
        fuse.enabled = false;
        //Time.timeScale = 1f;

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
            endGamePlatformFallScript.flashingWarning = false;


            remachButton.enabled = false;
            menuButton.enabled = false;
            Time.timeScale = 0f;

            buttonDisableDuration -= Time.unscaledDeltaTime;
            if (buttonDisableDuration <= 0)
            {
                remachButton.enabled = true;
                menuButton.enabled = true;
            }
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
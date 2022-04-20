using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class LootGrabber : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI score;
    public Transform respawnpoint;

    [SerializeField]
    public Transform Loot;

    public float loot = 0;

    public float percentageToRemoveGuard = 10;
    public float percentageToRemoveSpace = 20;

    [SerializeField] AudioSource bonkAudio;
    [SerializeField] AudioSource lootAudio;

    GemMechanic gemMechanic;
    [SerializeField] GameObject scorePopUp;

    PlayerControls playerControlsScript;

    private void Start()
    {
        gemMechanic = GetComponent<GemMechanic>();
        playerControlsScript = GetComponent<PlayerControls>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Loot")
        {
            lootAudio.Play();
            loot++;
            score.text = loot.ToString();
            Destroy(other.gameObject);
            ScoreTextPopUp();
        }

        if (other.transform.tag == "Guard")
        {
            if (playerControlsScript.vulnerable)
            {
                Debug.Log("THE AODSIBHRFO£WBGTIUGB£EI(U£EBGTIU£WGBTBW£IUW£BU(WE");
                PlayAudio();
                Transform clone;
                //gemMechanic.DropGem();
                //playerControlsScript.vulnerable = false;
                //playerControlsScript.canDoStuff = false;

                if (loot > 0)
                {
                    float pointsToRemove = Mathf.Ceil((loot * percentageToRemoveGuard) / 100);

                    for (int i = 0; i < pointsToRemove; i++)
                    {
                        clone = Instantiate(Loot, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Loot.rotation);
                        clone.gameObject.tag = "Loot";

                        clone.gameObject.GetComponentInChildren<Rigidbody>().useGravity = true;
                        loot--;
                        score.text = loot.ToString();
                    }
                }
                else if (loot <= 0)
                {
                    loot = 0;
                }
            }
        }
    }

    void PlayAudio()
    {
        Debug.Log("AUDIO IS BEING PLAYED DUMB DUMB");
        bonkAudio.Play();
        bonkAudio.Play();
        bonkAudio.Play();
        bonkAudio.Play();
        bonkAudio.Play();
        bonkAudio.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Killzone")
        {
            transform.position = respawnpoint.position;
            playerControlsScript.canDoStuff = false;
            if (loot > 0)
            {
                float pointsToRemove = Mathf.Ceil((loot * percentageToRemoveSpace) / 100);
                loot -= pointsToRemove;
                score.text = loot.ToString();
            }
            else if (loot <= 0)
            {
                loot = 0;
            }
        }
    }

    void ScoreTextPopUp()
    {
        Instantiate(scorePopUp, transform.position, scorePopUp.transform.rotation);
    }
}
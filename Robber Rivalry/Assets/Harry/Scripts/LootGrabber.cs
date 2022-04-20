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

    //[SerializeField] AudioSource bonkAudio;
    [SerializeField] AudioSource lootAudio;

    GemMechanic gemMechanic;
    [SerializeField] GameObject scorePopUp;

    PlayerControls playerControlsScript;
    public bool doDropLoot;

    private void Start()
    {
        gemMechanic = GetComponent<GemMechanic>();
        playerControlsScript = GetComponent<PlayerControls>();
        doDropLoot = false;
    }

    private void Update()
    {
        if (doDropLoot)
        {
            Debug.Log("THE AODSIBHRFO£WBGTIUGB£EI(U£EBGTIU£WGBTBW£IUW£BU(WE");
            //PlayAudio();
            Transform clone;
            //gemMechanic.DropGem();
            //playerControlsScript.vulnerable = false;
            //playerControlsScript.canDoStuff = false;

            if (loot > 0)
            {
                float pointsToRemove = Mathf.Ceil((loot * percentageToRemoveGuard) / 100);
                Debug.Log("Points to Remove: " + pointsToRemove);
                for (int i = 0; i < pointsToRemove; i++)
                {
                    Debug.Log("Loop is being called");
                    clone = Instantiate(Loot, new Vector3(transform.position.x + Random.Range(-1f, 2f), transform.position.y + 2f, transform.position.z + Random.Range(-1f, 2f)), Loot.rotation);
                    clone.gameObject.tag = "Loot";

                    clone.gameObject.GetComponentInChildren<Rigidbody>().useGravity = true;
                    clone.gameObject.GetComponentInChildren<Rigidbody>().isKinematic = false;
                    loot--;
                    score.text = loot.ToString();
                }
                Debug.Log("Outside Loop Debug function");
                doDropLoot=false;
            }
            else if (loot <= 0)
            {
                loot = 0;
            }
        }
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
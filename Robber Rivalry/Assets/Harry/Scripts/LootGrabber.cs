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

    private void Start()
    {
        gemMechanic = GetComponent<GemMechanic>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Loot")
        {
            lootAudio.Play();
            loot++;
            score.text = loot.ToString();
            Destroy(other.gameObject);
        }


        if (other.transform.tag == "Guard")
        {
            bonkAudio.Play();
            Transform clone;
            gemMechanic.DropGem();

            if (loot > 0)
            {
                float pointsToRemove = Mathf.Round(loot / percentageToRemoveGuard);
                loot -= pointsToRemove;
                score.text = loot.ToString();

                if (loot >= 5)
                {
                    for (int i = 0; i <= 5; i++)
                    {
                        clone = Instantiate(Loot, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Loot.rotation);
                        clone.gameObject.tag = "Loot";

                        clone.gameObject.GetComponentInChildren<Rigidbody>().useGravity = true;
                    }
                }
                else if (loot < 5)
                {

                    for (int i = 0; i < loot; i++)
                    {
                        clone = Instantiate(Loot, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Loot.rotation);
                        clone.gameObject.tag = "Loot";

                        clone.gameObject.GetComponentInChildren<Rigidbody>().useGravity = true;
                    }
                }

                transform.position = respawnpoint.position;

            }
            else if(loot <= 0)
            {
                loot = 0;
                transform.position = respawnpoint.position;
            }
        } 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Killzone")
        {
            transform.position = respawnpoint.position;
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
}
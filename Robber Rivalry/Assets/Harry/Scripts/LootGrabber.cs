using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LootGrabber : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI score;
    public Transform respawnpoint;

    public float loot = 0;

    public float percentageToRemoveGuard = 10;
    public float percentageToRemoveSpace = 20;

    [SerializeField] AudioSource bonkAudio;
    [SerializeField] AudioSource lootAudio;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Loot")
        {
            lootAudio.Play();
            loot++;
            score.text = loot.ToString();
            Destroy(other.gameObject);
        }

        if (other.transform.tag == "Guard")
        {
            bonkAudio.Play();
            transform.position = respawnpoint.position;
            if (loot > 0)
            {
                float pointsToRemove = Mathf.Round(loot / percentageToRemoveGuard);
                loot -= pointsToRemove;
                score.text = loot.ToString();
            }
            else if(loot <= 0)
            {
                loot = 0;
            }
        }
        if (other.transform.tag == "Killzone")
        {
            transform.position = respawnpoint.position;
            if (loot > 0)
            {
                float pointsToRemove = Mathf.Round(loot / percentageToRemoveSpace);
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
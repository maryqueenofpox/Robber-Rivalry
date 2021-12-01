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

    public float percentageToRemove = 10;


    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Loot")
        {
            loot++;
            score.text = loot.ToString();
            Destroy(other.gameObject);
        }

        if (other.transform.tag == "Guard")
        {
            transform.position = respawnpoint.position;
            if (loot > 0)
            {
                float pointsToRemove = Mathf.Round(loot / percentageToRemove);
                loot -= pointsToRemove;
                score.text = loot.ToString();
            }
        }
        if (other.transform.tag == "Killzone")
        {
            transform.position = respawnpoint.position;
            if (loot > 5)
            {
                loot -= 5;
                score.text = loot.ToString();
            }
            else if (loot <= 5)
            {
                loot = 0;
            }
        }
    }
}
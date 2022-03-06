using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemMechanic : MonoBehaviour
{
    public bool isCarryingGem = false;
    [Header("Gem Score")]
    [SerializeField] float gemPointIncrease;
    public float timeUntilScoreIncrease = 4.0f;
    float originalTimeUntilScoreIncrease;
    Transform gemTransform;
    bool isGem;
    Transform gemChild;
    LootGrabber lootGrabber;
    // Start is called before the first frame update
    void Start()
    {
        lootGrabber = GetComponent<LootGrabber>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isCarryingGem)
        {
            if (transform.childCount < 2)
                isCarryingGem = false;
            else
            {
                timeUntilScoreIncrease -= Time.deltaTime;
                if (timeUntilScoreIncrease <= 0.0f)
                {
                    lootGrabber.loot += gemPointIncrease;
                    lootGrabber.score.text = lootGrabber.loot.ToString();
                    timeUntilScoreIncrease = originalTimeUntilScoreIncrease;
                }
            }
        }
    }
}

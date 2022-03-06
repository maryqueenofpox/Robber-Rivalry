using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemMechanic : MonoBehaviour
{
    public bool isCarryingGem = false;
    [SerializeField] float gemPointIncrease;
    public float timeUntilScoreIncrease = 4.0f;
    float originalTimeUntilScoreIncrease;
    public Transform gemTransform { get; set; }
    bool isGem;
    Transform gemChild;
    LootGrabber lootGrabber;

    // Start is called before the first frame update
    void Start()
    {
        lootGrabber = GetComponent<LootGrabber>();

        originalTimeUntilScoreIncrease = timeUntilScoreIncrease;
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

    public void PickUpGem()
    {
        if (!isCarryingGem)
        {
            if (isGem)
            {
                gemTransform.parent = transform;
                gemChild = transform.Find("Gem");
                gemChild.localPosition = new Vector3(0, 2.5f, 0);
                isCarryingGem = true;
                isGem = false;
            }
        }
    }

    public void DropGem()
    {
        if (isCarryingGem)
        {
            gemChild.localPosition = transform.TransformDirection(Vector3.forward * 1);
            gemChild.parent = null;
            isCarryingGem = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Gem"))
        {
            gemTransform = other.GetComponent<Transform>();
            isGem = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Gem"))
        {
            isGem = false;
        }
    }
}
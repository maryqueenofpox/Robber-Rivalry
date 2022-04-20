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

    [SerializeField] GameObject scorePopUp;

    // This is test for github

    // Start is called before the first frame update
    void Start()
    {
        lootGrabber = GetComponent<LootGrabber>();
        // so there is a change
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
                    ScoreTextPopUp();
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
            gemChild.localPosition = transform.TransformDirection(Vector3.forward * 1.5f);
            gemChild.localPosition = new Vector3(gemChild.localPosition.x, 1f, gemChild.localPosition.z);
            gemChild.parent = null;
            isCarryingGem = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Gem"))
        {
            gemTransform = collision.gameObject.GetComponent<Transform>();
            isGem = true;
            PickUpGem();
        }
    }

    void ScoreTextPopUp()
    {
        Instantiate(scorePopUp, transform.position, scorePopUp.transform.rotation);
    }
}
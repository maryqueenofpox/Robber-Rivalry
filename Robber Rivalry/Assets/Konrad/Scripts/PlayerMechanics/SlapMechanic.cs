using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlapMechanic : MonoBehaviour
{
    [SerializeField] float slapFoce = 5000f;
    [SerializeField] float slapCooldown = 3f;
    public bool canSlap;
    [SerializeField] float timeToSetSlapToFalse = 1f;
    float originalSlapToFalse;
    float originalSlapCooldown;
    [SerializeField] GameObject SlapTrail;
    [SerializeField] GameObject SlapTrail2;

    PlayerAnimations playerAnimationsScript;

    PlayerMovement playerMovement;
    PlayerControls controls;
    ForceField forceField;
    GemMechanic gemMechanic;
    bool isPlayer;

    public bool doingSlap;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimationsScript = GetComponent<PlayerAnimations>();

        SlapTrail.SetActive(false);
        SlapTrail2.SetActive(false);

        originalSlapToFalse = timeToSetSlapToFalse;
        originalSlapCooldown = slapCooldown;

        canSlap = true;
        doingSlap = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Doing Slap: " + doingSlap);

        if (playerAnimationsScript.anim.GetBool("isSlapping") == true)
        {
            timeToSetSlapToFalse -= Time.deltaTime;
            Debug.Log("if playeranimationscscript"); // 17 times
            //if (timeToSetSlapToFalse <= 0.1f)
            //YeetThePlayer();

            if (timeToSetSlapToFalse <= 0f)
            {
                Debug.Log("timeToSetSlapToFalse");
                timeToSetSlapToFalse = originalSlapToFalse;
                playerAnimationsScript.IsSlappingAnimation(false);
                SlapTrail.SetActive(false);
                SlapTrail2.SetActive(false);
                YeetThePlayer();
            }
        }

        if (!canSlap)
        {
            slapCooldown -= Time.deltaTime;
            Debug.Log("!canSlap");
            if (slapCooldown <= 0.0f)
            {
                Debug.Log("!canSlap, slapCooldown");
                canSlap = true;
                doingSlap = false;
                slapCooldown = originalSlapCooldown;
            }
        }

        /*
        if (doTheSlap)
        {
            DoTheSlap();
            //controls.slapOncePleaseForTheLoveOfGod = true;
        }
        */
    }

    public void DoTheSlap()
    {
        Debug.Log("DoTheSlap");

        if (canSlap)
        {
            doingSlap = true;
            playerAnimationsScript.IsSlappingAnimation(true);
            SlapTrail.SetActive(true);
            SlapTrail2.SetActive(true);
        }
        else
            return;
    }

    void YeetThePlayer()
    {
        Debug.Log("YeetThePlayer");

        if (isPlayer && controls.vulnerable)
        {
            if (forceField.enabled == true)
            {
                forceField.enabled = false;
                canSlap = false;
                doingSlap = false;
                return;
            }
            else
            {
                playerMovement.rb.AddForce(transform.forward * slapFoce);
                playerMovement.rb.AddForce(transform.up * 1000000f);
                controls.isStunned = true;

                if (gemMechanic.isCarryingGem)
                {
                    gemMechanic.DropGem();
                    playerMovement.transform.Find("Gem").transform.parent = null;
                    gemMechanic.isCarryingGem = false;
                }

                canSlap = false;
                doingSlap = false;
            }
        }
        else
            doingSlap = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerMovement = other.GetComponent<PlayerMovement>();
            forceField = other.GetComponent<ForceField>();
            gemMechanic = other.GetComponent<GemMechanic>();
            controls = other.GetComponent<PlayerControls>();
            isPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayer = false;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlapMechanic : MonoBehaviour
{
    [SerializeField] float slapFoce = 5000f;
    [SerializeField] float slapCooldown = 3f;
    bool canSlap;
    [SerializeField] float timeToSetSlapToFalse = 1f;
    float originalSlapToFalse;
    public bool doTheSlap = false;
    float originalSlapCooldown;
    [SerializeField] GameObject SlapTrail;
    [SerializeField] GameObject SlapTrail2;

    PlayerAnimations playerAnimationsScript;

    PlayerMovement playerMovement;
    PlayerControls controls;
    ForceField forceField;
    GemMechanic gemMechanic;
    bool isPlayer;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimationsScript = GetComponent<PlayerAnimations>();

        SlapTrail.SetActive(false);
        SlapTrail2.SetActive(false);

        originalSlapToFalse = timeToSetSlapToFalse;
        originalSlapCooldown = slapCooldown;

        canSlap = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerAnimationsScript.anim.GetBool("isSlapping") == true)
        {
            timeToSetSlapToFalse -= Time.deltaTime;
            if (timeToSetSlapToFalse <= 0f)
            {
                timeToSetSlapToFalse = originalSlapToFalse;
                playerAnimationsScript.IsSlappingAnimation(false);
                SlapTrail.SetActive(false);
                SlapTrail2.SetActive(false);
            }
        }
        if (!canSlap)
        {
            slapCooldown -= Time.deltaTime;
        }

        if (slapCooldown <= 0.0f)
        {
            canSlap = true;
            slapCooldown = originalSlapCooldown;
        }

        if (doTheSlap)
            DoTheSlap();
    }

    public void DoTheSlap()
    {
        if (canSlap)
        {
            playerAnimationsScript.IsSlappingAnimation(true);
            SlapTrail.SetActive(true);
            SlapTrail2.SetActive(true);

            if (timeToSetSlapToFalse <= 0.1f)
            {
                if (isPlayer && controls.vulnerable)
                {
                    if (forceField.enabled == true)
                    {
                        forceField.enabled = false;
                        canSlap = false;
                        doTheSlap = false;
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
                        doTheSlap = false;
                    }
                }
                doTheSlap = false;            
            }
        }
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
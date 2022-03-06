using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlapMechanic : MonoBehaviour
{
    [SerializeField] float slapFoce = 5000f;
    [SerializeField] float slapCooldown = 3f;
    bool canSlap = true;
    [SerializeField]
    float timeToSetSlapToFalse = 1f;
    float originalSlapToFalse;
    [SerializeField]
    float durationToIncreaseBy = 0.1f;
    bool doTheSlap = false;
    float originalSlapCooldown;

    Animator anim;

    public PlayerControls controls { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        originalSlapToFalse = timeToSetSlapToFalse;

        originalSlapToFalse = timeToSetSlapToFalse;
        originalSlapCooldown = slapCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetBool("isSlapping") == true)
        {
            timeToSetSlapToFalse -= Time.deltaTime;
            if (timeToSetSlapToFalse <= 0f)
            {
                timeToSetSlapToFalse = originalSlapToFalse;
                anim.SetBool("isSlapping", false);
            }
        }
        /*
        if (isStunned && vulnerable)
        {
            stunDuration -= Time.deltaTime;
            anim.SetBool("gotSlapped", true);
            if (stunDuration <= 0)
            {
                anim.SetBool("gotSlapped", false);
                isStunned = false;
                vulnerable = false;
                stunDuration = originalStunDuration;
                anim.SetBool("idle", true);
            }
        }
        */
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

    void DoTheSlap()
    {
        /*
        if (canSlap && !isStunned)
        {
            anim.SetBool("isSlapping", true);
            if (timeToSetSlapToFalse <= 0.1f)
            {
                if (isPlayer && controls.vulnerable)
                {
                    if (controls.forceField.enabled == true)
                    {
                        controls.forceField.enabled = false;
                        canSlap = false;
                        doTheSlap = false;
                        return;
                    }
                    else
                    {
                        controls.rb.AddForce(transform.forward * slapFoce);
                        //if (controls.vulnerable)
                        controls.isStunned = true;

                        if (controls.isCarryingGem)
                        {
                            controls.transform.Find("Gem").transform.localPosition = controls.transform.TransformDirection(-Vector3.forward * 2);
                            controls.transform.Find("Gem").transform.parent = null;
                            controls.isCarryingGem = false;
                        }

                        canSlap = false;
                        doTheSlap = false;
                    }
                }
                doTheSlap = false;
            }
        }
        */
    }
}

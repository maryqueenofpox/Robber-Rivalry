using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    [SerializeField] GameObject wetFloorSign;
    [SerializeField] GameObject honeyGrenade;
    public GameObject rayGun;
    [SerializeField] GameObject rayBullet;
    [SerializeField] GameObject magnetField;
    [SerializeField] float bulletVelocity = 50f;
    [SerializeField] float magnetTimer;
    [SerializeField] float shieldTimer;
    float originalTimerMaget;
    float originalTimerShield;
    [SerializeField] float grenadeUpwardForce = 1000000f;
    [SerializeField] float grenadeForwardForce = 1000000f;
    public bool canUseAbility { get; set; }
    bool pickRandomAbility;

    public int randomAbility { get; set; }

    [SerializeField] AudioSource powerUpAudio;

    ForceField forceFieldScript;

    PlayerUI playerUIScript;

    // Start is called before the first frame update
    void Start()
    {
        pickRandomAbility = false;
        canUseAbility = false;
        rayGun.SetActive(false);
        forceFieldScript = GetComponent<ForceField>();
        forceFieldScript.enabled = false;
        playerUIScript = GetComponent<PlayerUI>();
        rayGun.SetActive(false);
        magnetField.SetActive(false);
        originalTimerMaget = magnetTimer;
        originalTimerShield = shieldTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (pickRandomAbility)
        {
            randomAbility = Random.Range(0, 5);
            playerUIScript.number = randomAbility;
            pickRandomAbility = false;
            canUseAbility = true;

            if (randomAbility == 0)
                rayGun.SetActive(true);
            else
                rayGun.SetActive(false);
        }

        if (magnetField.activeSelf)
        {
            magnetTimer -= Time.deltaTime;
            if (magnetTimer < 0)
            {
                magnetField.SetActive(false);
                
            }
        }

        if (forceFieldScript.enabled == true)
        {
            shieldTimer -= Time.deltaTime;
            if (shieldTimer < 0)
            {
                forceFieldScript.enabled = false;
            }
        }

        else
            magnetTimer = originalTimerMaget;
            originalTimerShield = shieldTimer;
    }

    public void Ability()
    {
        if (canUseAbility)
        {
            switch (randomAbility)
            {
                case 0:
                    ShootRayGun();
                    canUseAbility = false;
                    break;
                case 1:
                    SpawnObject();
                    canUseAbility = false;
                    break;
                case 2:
                    ThrowGrenade();
                    canUseAbility = false;
                    break;
                case 3:
                    forceFieldScript.enabled = true;
                    playerUIScript.Shield(false);
                    canUseAbility = false;
                    break;
                case 4:
                    UseMagnet();
                    canUseAbility = false;
                    break;
                default:
                    break;
            }
        }
    }

    void ShootRayGun()
    {
        if (rayGun.activeSelf == true)
        {
            GameObject bullet;
            bullet = Instantiate(rayBullet, rayGun.transform.position, transform.rotation);

            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletVelocity * Time.deltaTime);

            rayGun.SetActive(false);
            playerUIScript.PewGun(false);
        }
    }

    void SpawnObject()
    {
        //Instantiate(wetFloorSign, new Vector3(transform.position.x + transform.forward.x + 1f, 0f, transform.position.z + transform.forward.z + 1f), transform.rotation);
        Instantiate(wetFloorSign, transform.position + (transform.forward * 2f), transform.rotation);
        AstarPath.active.Scan();
        playerUIScript.Sign(false);
    }

    void ThrowGrenade()
    {
        GameObject grenade;
        grenade = Instantiate(honeyGrenade, transform.position + (transform.forward * 2f), transform.rotation);
        grenade.GetComponent<Rigidbody>().AddForce(transform.up * grenadeUpwardForce * Time.deltaTime);
        grenade.GetComponent<Rigidbody>().AddForce(transform.forward * grenadeForwardForce * Time.deltaTime);
        
       
        playerUIScript.Honey(false);
    }
    
    void UseMagnet()
    {
        magnetField.SetActive(true);
        playerUIScript.Magnet(false);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("PowerUp"))
        {
            powerUpAudio.Play();
            pickRandomAbility = true;
        }
    }
}

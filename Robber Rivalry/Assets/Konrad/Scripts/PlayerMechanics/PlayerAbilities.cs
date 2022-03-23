using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    [SerializeField] GameObject wetFloorSign;
    [SerializeField] GameObject honeyGrenade;
    public GameObject rayGun;
    [SerializeField] GameObject rayBullet;
    [SerializeField] Transform magnetField;
    [SerializeField] float bulletVelocity = 50f;
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
        }
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
            Debug.Log("Gun Active");
            GameObject bullet;
            bullet = Instantiate(rayBullet, rayGun.transform.position, transform.rotation);

            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletVelocity * Time.deltaTime);

            rayGun.SetActive(false);
        }
    }

    void SpawnObject()
    {
        Instantiate(wetFloorSign, new Vector3(transform.position.x + transform.forward.x, 0f, transform.position.z + transform.forward.z), transform.rotation);
        AstarPath.active.Scan();
    }

    void ThrowGrenade()
    {
        GameObject grenade;
        grenade = Instantiate(honeyGrenade, transform.position, transform.rotation);
        grenade.GetComponent<Rigidbody>().AddForce(transform.up * 5000);
    }

    void EnableForcefield()
    {

    }
    
    void UseMagnet()
    {
        Transform clone;
        clone = Instantiate(magnetField, transform.position, transform.rotation);
        clone.transform.tag = "MagnetField";
        clone.transform.parent = transform;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("PowerUp"))
        {
            powerUpAudio.Play();
            pickRandomAbility = true;
        }
    }
}

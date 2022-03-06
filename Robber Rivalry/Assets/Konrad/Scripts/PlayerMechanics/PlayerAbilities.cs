using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    [SerializeField] GameObject wetFloorSign;
    public GameObject rayGun;
    [SerializeField] GameObject rayBullet;
    [SerializeField] float bulletVelocity = 50f;
    public bool canUseAbility { get; set; }
    [SerializeField] AudioSource powerUpAudio;


    // Start is called before the first frame update
    void Start()
    {
        canUseAbility = false;
        rayGun.SetActive(false);
        //forceFieldScript.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Ability()
    {
        if (canUseAbility)
        {
            SpawnObject();
            canUseAbility = false;
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
        Instantiate(wetFloorSign, new Vector3(transform.position.x + transform.forward.x, 0f, transform.position.z + transform.forward.z), transform.rotation); //wetFloorSign.transform.rotation);
        AstarPath.active.Scan();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("PowerUp"))
        {
            powerUpAudio.Play();
            canUseAbility = true;
        }
    }
}

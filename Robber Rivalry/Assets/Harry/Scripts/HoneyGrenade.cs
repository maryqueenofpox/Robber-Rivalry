using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoneyGrenade : MonoBehaviour
{

    float activeDuration = 1.75f;
    [SerializeField] float throwForce = 5f;
    [SerializeField] Transform HoneySplat;
    [SerializeField]
    public float yspeed = 10.0f;
    [SerializeField]
    public float forwardspeed = 10.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("Player"))
        {
            Physics.IgnoreCollision(item.GetComponent<Collider>(), GetComponent<Collider>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        activeDuration -= Time.deltaTime;
        if (activeDuration <= 0f)
        {
            Transform clone;
            clone = Instantiate(HoneySplat, transform.position, HoneySplat.rotation);
            //clone.gameObject.tag = "HoneySplat";
            Destroy(gameObject);
        }
       
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Wall")
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = true;

        }
    }

}

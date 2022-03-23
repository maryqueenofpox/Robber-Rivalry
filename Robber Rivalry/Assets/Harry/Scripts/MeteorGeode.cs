using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorGeode : MonoBehaviour
{
    [SerializeField]
    public float yspeed = 10.0f;
    [SerializeField]
    public float xspeed = 10.0f;
    [SerializeField]
    public float Negativexspeed = 10.0f;
    [SerializeField]
    float rotationSpeed = 60f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0, Space.World);
        GetComponent<Rigidbody>().AddForce(Vector2.up * -yspeed * Time.deltaTime);
        GetComponent<Rigidbody>().AddForce(Vector2.right * xspeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other)
    {
      
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "LootReplenish")
        {
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "Wall")
        {
            xspeed = 60;
            rotationSpeed = 60;
           // transform.parent = other.transform;
        }
        else if (other.gameObject.tag == "Loot")
        {
            xspeed = 0;

        }
        else
        {

            xspeed = 0;
            yspeed = 0;
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            transform.parent = other.transform;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorGeode : MonoBehaviour
{

    public float speed = 10.0f;
    [SerializeField] float rotationSpeed = 60f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0, Space.World);
        GetComponent<Rigidbody>().AddForce(Vector2.up * -speed * Time.deltaTime);
        GetComponent<Rigidbody>().AddForce(Vector2.right * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other)
    {
      
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "LootReplenish")
        {
            Destroy(gameObject);
        }

        else
        {
            speed = 0;
        }
    }
}

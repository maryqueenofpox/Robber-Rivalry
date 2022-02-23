using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Guard_Control : MonoBehaviour
{
    float waitTime = 5f;

    [SerializeField]
    public float VisionDistance = 10.0f;
    [SerializeField]
    public float VisionAngle = 45.0f;

    [SerializeField]
    Transform player1;

    [SerializeField]
    Transform player2;

    [SerializeField]
    Transform player3;

    [SerializeField]
    Transform player4;

    AIPath aipath;


    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.transform.tag == "Player")
        {
            waitTime = 0.0f;

            if (waitTime < 5)
            {
                GetComponent<AIDestinationSetter>().enabled = false;
                GetComponent<Patrol>().enabled = true;
                waitTime -= Time.deltaTime;
            }
            else
            {
                return;
            }
    
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
		if (IsPlayer1InVisionDistance() == true && IsPlayer1InVisionAngle() == true)
		{
            StartCoroutine(Wait());
            GetComponent<Patrol>().enabled = false;
            GetComponent<AIDestinationSetter>().enabled = true;
            GetComponent<AIDestinationSetter>().target = player1;           

            aipath.maxSpeed = 10f;           
        }
        else if (IsPlayer1InVisionDistance() == false && IsPlayer1InVisionAngle() == false)
        {
                      
            GetComponent<Patrol>().enabled = true;
            GetComponent<AIDestinationSetter>().enabled = false;
            GetComponent<Patrol>().enabled = true;
            aipath.maxSpeed = 3f;
        }
        else if (IsPlayer2InVisionDistance() == true && IsPlayer2InVisionAngle() == true)
        {
            StartCoroutine(Wait());
            GetComponent<Patrol>().enabled = false;
            GetComponent<AIDestinationSetter>().enabled = true;
            GetComponent<AIDestinationSetter>().target = player2;
            aipath.maxSpeed = 10f;
        }
        else if (IsPlayer2InVisionDistance() == false && IsPlayer2InVisionAngle() == false)
        {
           
            GetComponent<Patrol>().enabled = true;
            GetComponent<AIDestinationSetter>().enabled = false;
            GetComponent<Patrol>().enabled = true;
            aipath.maxSpeed = 3f;
        }
        else if (IsPlayer3InVisionDistance() == true && IsPlayer3InVisionAngle() == true)
        {
            StartCoroutine(Wait());
            GetComponent<Patrol>().enabled = false;
            GetComponent<AIDestinationSetter>().enabled = true;
            GetComponent<AIDestinationSetter>().target = player3;
            aipath.maxSpeed = 10f;
        }
        else if (IsPlayer3InVisionDistance() == false && IsPlayer3InVisionAngle() == false)
        {
            
            GetComponent<Patrol>().enabled = true;
            GetComponent<AIDestinationSetter>().enabled = false;
            GetComponent<Patrol>().enabled = true;
            aipath.maxSpeed = 3f;
        }
        else if (IsPlayer4InVisionDistance() == true && IsPlayer4InVisionAngle() == true)
        {
            StartCoroutine(Wait());
            GetComponent<Patrol>().enabled = false;
            GetComponent<AIDestinationSetter>().enabled = true;
            GetComponent<AIDestinationSetter>().target = player4;
            aipath.maxSpeed = 10f;
        }
        else if (IsPlayer4InVisionDistance() == false && IsPlayer4InVisionAngle() == false)
        {
           
            GetComponent<Patrol>().enabled = true;
            GetComponent<AIDestinationSetter>().enabled = false;
            GetComponent<Patrol>().enabled = true;
            aipath.maxSpeed = 3f;
        }
    }

    protected bool IsPlayer1InVisionDistance()
    {
        float distanceToPlayer = (player1.transform.position - transform.position).magnitude;
        return distanceToPlayer <= VisionDistance;
    }

    protected bool IsPlayer1InVisionAngle()
    {
        Vector3 playerDirection = player1.transform.position - transform.position;
        playerDirection = playerDirection.normalized;

        float dot = Vector3.Dot(playerDirection, transform.forward);
        float angleToPlayer = Mathf.Acos(dot) * Mathf.Rad2Deg;
        return angleToPlayer <= VisionAngle;
    }

    protected bool IsPlayer2InVisionDistance()
    {
        float distanceToPlayer = (player2.transform.position - transform.position).magnitude;
        return distanceToPlayer <= VisionDistance;
    }

    protected bool IsPlayer2InVisionAngle()
    {
        Vector3 playerDirection = player2.transform.position - transform.position;
        playerDirection = playerDirection.normalized;

        float dot = Vector3.Dot(playerDirection, transform.forward);
        float angleToPlayer = Mathf.Acos(dot) * Mathf.Rad2Deg;
        return angleToPlayer <= VisionAngle;
    }

    protected bool IsPlayer3InVisionDistance()
    {
        float distanceToPlayer = (player3.transform.position - transform.position).magnitude;
        return distanceToPlayer <= VisionDistance;
    }

    protected bool IsPlayer3InVisionAngle()
    {
        Vector3 playerDirection = player3.transform.position - transform.position;
        playerDirection = playerDirection.normalized;

        float dot = Vector3.Dot(playerDirection, transform.forward);
        float angleToPlayer = Mathf.Acos(dot) * Mathf.Rad2Deg;
        return angleToPlayer <= VisionAngle;
    }
    protected bool IsPlayer4InVisionDistance()
    {
        float distanceToPlayer = (player4.transform.position - transform.position).magnitude;
        return distanceToPlayer <= VisionDistance;
    }

    protected bool IsPlayer4InVisionAngle()
    {
        Vector3 playerDirection = player4.transform.position - transform.position;
        playerDirection = playerDirection.normalized;

        float dot = Vector3.Dot(playerDirection, transform.forward);
        float angleToPlayer = Mathf.Acos(dot) * Mathf.Rad2Deg;
        return angleToPlayer <= VisionAngle;
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
    }
    IEnumerator Wait3()
    {
        yield return new WaitForSeconds(3);
    }
}

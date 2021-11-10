using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Guard_Pathfinding : MonoBehaviour
{
    [SerializeField]
    public Transform player1;
    public Transform player2;
    public Transform player3;
    public Transform player4;

    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    void Start()
    {
        seeker = GetComponent < Seeker >
            rb = GetComponent < Rigidbody2D >

            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

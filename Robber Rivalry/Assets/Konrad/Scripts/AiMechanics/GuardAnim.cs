using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardAnim : MonoBehaviour
{
    public Animator anim;
    Pathfinding.AIDestinationSetter ai;

    float distanceToTarget;
    [SerializeField] float optimalAnimationTarget;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        ai = GetComponent<Pathfinding.AIDestinationSetter>();
    }

    // Update is called once per frame
    void Update()
    {
        distanceToTarget = Vector3.Distance(transform.position, ai.target.position);

        if (distanceToTarget <= optimalAnimationTarget)
        {
            anim.SetBool("isSmashing", true);
        }
        else
        {
            anim.SetBool("isSmashing", false);
        }
    }
}
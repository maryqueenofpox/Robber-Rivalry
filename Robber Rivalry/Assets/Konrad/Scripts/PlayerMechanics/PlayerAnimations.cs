using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    public Animator anim { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void GotSlappedAnim(bool gotSlapped)
    {
        anim.SetBool("gotSlapped", gotSlapped);
    }

    public void IdleAnimation(bool idle)
    {
        anim.SetBool("idle", idle);
    }

    public void IsSlappingAnimation(bool isSlapping)
    {
        anim.SetBool("isSlapping", isSlapping);
    }

    public void IsDashingAnimation(bool isDashing)
    {
        anim.SetBool("isDashing", isDashing);
    }

    public void IsMovingAnimation(bool isMoving)
    {
        anim.SetBool("isMoving", isMoving);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanController : EntityController
{
    public bool is_moving;

    private Animator animator;
    private bool is_running, is_patroling, alerted;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();

        rb            = GetComponent<Rigidbody>();
        animator      = GetComponent<Animator>();
        target        = new Vector3();
        MovementSpeed = 2;
    }

    void FixedUpdate()
    {
        if (is_moving)
        {
            MoveToTarget();
        }
    }


    public new void MoveToTarget()
    {
        animator.SetBool("is_walking", true);
        if (!base.MoveToTarget())
        {
            is_moving = false;
            is_running = false;
            animator.SetBool("is_walking", false);
            animator.SetBool("is_running", is_running);
        }
    }
}

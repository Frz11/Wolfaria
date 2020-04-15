using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanController : EntityController
{
    public bool is_moving;

    private Animator animator;
    private bool is_running, is_patroling, alerted;

    // Start is called before the first frame update
    void Start()
    {
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

    private void CalculateRotation()
    {
        Vector3 local = transform.InverseTransformPoint(target);
        float angle = Mathf.Atan2(local.x, local.z) * Mathf.Rad2Deg;
        Vector3 eulerAngleVelocity = new Vector3(0, angle, 0);

        deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.fixedDeltaTime * 5);
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

    private void MoveHuman(float moving_speed = 2)
    {
        if (System.Math.Abs(transform.position.x - target.x) > 1 || System.Math.Abs(transform.position.z - target.z) > 1)
        {
            this.CalculateRotation();

            rb.MovePosition(Vector3.MoveTowards(transform.position, target, moving_speed * Time.deltaTime));
            rb.MoveRotation(rb.rotation * deltaRotation);
        }
        else
        {
            is_moving = false;
            is_running = false;
            animator.SetBool("is_walking", false);
            animator.SetBool("is_running", is_running);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DogController : EntityController
{
    private GameObject Player;
    private Animation anim;

    [SerializeField]
    private float maxPlayerDistance = 2f;
    private bool is_moving, is_running, patrol, alerted;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();

        Player        = GameObject.Find("Player");
        anim          = GetComponent<Animation>();
        rb            = GetComponent<Rigidbody>();
        MovementSpeed = 3.5f;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (alerted)
        {
            target = Player.transform.position;
            MoveToTarget();

            return;
        }

        if (Vector3.Distance(transform.position, Player.transform.position) <= maxPlayerDistance)
        {
            Vector3 rayDirection = Player.transform.position - transform.position;
            Ray ray = new Ray(transform.position, rayDirection);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.name == "Player")
                {
                    alerted    = true;
                    is_running = true;
                }
            }
        }
    }


    private new void MoveToTarget()
    {
        is_running = true;
        anim.Play("Run");

        if (!base.MoveToTarget())
        {
            is_moving = false;
            is_running = false;
            alerted = false;

            anim.Play("Idle");
        }
    }

}

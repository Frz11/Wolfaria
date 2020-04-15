using System;
using System.Collections;
using System.Reflection;
using UnityEngine;

public class SheepController : EntityController
{
    private Animation anim;
    private float x_min, x_max, z_min, z_max;
    private GameObject Player;
    private float fovAngle = 30f;
    private float maxPlayerDistance = 3f;

    [SerializeField]
    private int walk_range = 10;
    [SerializeField]
    private bool is_running, is_moving;
    [SerializeField]
    private string LevelControllerName;
    private GameObject LevelControllerGO;

    private LevelController LevelControllerInstance;

    // Start is called before the first frame update
    void Start()
    {

        Player            = GameObject.Find("Player");
        LevelControllerGO = GameObject.Find("LevelController");
        MovementSpeed     = 4f;

        switch (LevelControllerName)
        {
            case "TutorialController":
                LevelControllerInstance = LevelControllerGO.GetComponent<TutorialController>();
                break;

            default:
                LevelControllerInstance = LevelControllerGO.GetComponent<LevelController>();
                break;
        }

        is_running = false;
        anim       = GetComponent<Animation>();
        rb         = GetComponent<Rigidbody>();
        x_min      = transform.position.x - walk_range;
        x_max      = transform.position.x + walk_range;
        z_min      = transform.position.z - walk_range;
        z_max      = transform.position.z + walk_range;

        generateTarget();
        CalculateRotation();

        anim.Play("Idle");

        StartCoroutine(Decide());
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (is_running)
        {
            MoveToTarget();
            return;
        }

        RaycastHit hit;
        Vector3 rayDirection = Player.transform.position - transform.position;
        Debug.DrawLine(Player.transform.position, transform.position, Color.red);
        float distance = Vector3.Distance(Player.transform.position, transform.position);
        float angle    = Vector3.Angle(rayDirection, transform.forward);

        if (angle <= fovAngle && distance <= maxPlayerDistance)
        {
            Ray ray = new Ray(transform.position, rayDirection);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.name == "Player")
                {
                    target = transform.position + (-transform.forward) * 12f;
                    Debug.Log(target);
                    is_running = true;

                    LevelControllerInstance.DetectedPlayerNumber++;
                } 
            }
        }

        if (is_moving)
        {
            MoveToTarget();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            Invoke("Kill", 0.1f);
        }
        else if (other.gameObject.tag == "Wall")
        {
            is_moving  = false;
            is_running = false;
            anim.Play("Idle");
        }
      
    }

    protected void Kill()
    {
        LevelControllerInstance.SheepNumber--;
        LevelControllerInstance.ShowSheeps();

        Destroy(this.gameObject);
    }


    IEnumerator Decide()
    {
        while (true)
        { 
            if (is_running == false)
            {
                int action = UnityEngine.Random.Range(0, 100);

                if (action <= 10)
                {
                    anim.Play("Idle");
                }
                else if (action > 10 && action <= 35)
                {
                    anim.Play("GrazeA");
                }
                else if (action > 35 && action <= 70)
                {
                    anim.Play("GrazeB");
                }
                else if (action > 70)
                {
                    this.generateTarget();

                    is_moving = true;
                    anim.Play("Walk");
                }
            }
           
            yield return new WaitForSeconds(15f);
        }
    }

    private void generateTarget()
    {
        int x  = UnityEngine.Random.Range(Mathf.RoundToInt(x_min), Mathf.RoundToInt(x_max));
        int z  = UnityEngine.Random.Range(Mathf.RoundToInt(z_min), Mathf.RoundToInt(z_max));

        target = new Vector3(x, transform.position.y, z);
    }

    private new void MoveToTarget()
    {
        anim.Play("Run");

        if (!base.MoveToTarget())
        {
            is_moving  = false;
            is_running = false;
            anim.Play("Idle");
        }
    }

}

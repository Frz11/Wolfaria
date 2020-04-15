using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : EntityController
{
    public GameObject WolfSprite;
    public Camera Camera;
    public float varianceInDistances = 5.0F;
    public float minPinchSpeed = 5.0F;
    public int zoomSpeed = 4;
    public bool Blocked = false;
    public bool is_dead;


    private Animator WolfAnimator;

    [SerializeField]
    private bool is_running;
    [SerializeField]
    private string LevelControllerName;

    [SerializeField]
    private int hearts = 3;

    private GameObject LevelControllerGO;
    private LevelController LevelControllerInstance;
    



    // Start is called before the first frame update
    void Start()
    {
        MovementSpeed     = 5f;
        LevelControllerGO = GameObject.Find("LevelController");

        switch (LevelControllerName)
        {
            case "TutorialController":
                LevelControllerInstance = LevelControllerGO.GetComponent<TutorialController>();
                break;

            default:
                LevelControllerInstance = LevelControllerGO.GetComponent<LevelController>();
                break;
        }

        WolfAnimator = WolfSprite.GetComponent<Animator>();
        rb = this.gameObject.GetComponent<Rigidbody>();

        rb.isKinematic = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (is_dead)
        {
            WolfAnimator.SetBool("is_running", false);
            WolfAnimator.SetBool("is_dead", true);
            this.gameObject.SetActive(false);
            return;
        }

        WolfAnimator.SetBool("is_running", is_running);

        if (move_to_target)
        {
            MoveToTarget();
            return;
        }

        if (Application.platform != RuntimePlatform.Android && Input.GetMouseButton(0) && !Blocked)
        {
            Vector3 mousePosition = Input.mousePosition;
            Ray ray               = Camera.ScreenPointToRay(mousePosition);

            MovePlayer(ray);

            return;
        }

        if (Application.platform == RuntimePlatform.Android && Input.touchCount > 0)
        {
            if (Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(1).phase == TouchPhase.Moved)
            {
                is_running = false;
                ZoomCamera();

            }
            else if (Input.touchCount == 1 && !Blocked)
            {
                Touch touch = Input.GetTouch(0);
                Ray ray = Camera.ScreenPointToRay(touch.position);

                MovePlayer(ray);
            }

            return;

        }

        is_running = false;
    }

    private void ZoomCamera()
    {
        Vector2 curDist = Input.GetTouch(0).position - Input.GetTouch(1).position;
        Vector2 prevDist = ((Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition) - (Input.GetTouch(1).position - Input.GetTouch(1).deltaPosition));
        float touchDelta = curDist.magnitude - prevDist.magnitude;
        float speedTouch0 = Input.GetTouch(0).deltaPosition.magnitude / Input.GetTouch(0).deltaTime;
        float speedTouch1 = Input.GetTouch(1).deltaPosition.magnitude / Input.GetTouch(1).deltaTime;

        Vector3 camPos = Camera.gameObject.transform.position;
        if (touchDelta + varianceInDistances <= 1 && speedTouch0 > minPinchSpeed && speedTouch1 > minPinchSpeed)
        {
            Camera.gameObject.transform.position = new Vector3(camPos.x,Mathf.Clamp(camPos.y + (1 * zoomSpeed), 6, 10),camPos.z);
        }

        if (touchDelta + varianceInDistances > 1 && speedTouch0 > minPinchSpeed && speedTouch1 > minPinchSpeed)
        {
            Camera.gameObject.transform.position = new Vector3(camPos.x, Mathf.Clamp(camPos.y - (1 * zoomSpeed), 6, 10), camPos.z);
        }

    }

    private new void MoveToTarget()
    {
        is_running = true;
        if (!base.MoveToTarget())
        {
            move_to_target = false;
            is_running = false;
            WolfAnimator.SetBool("is_running", is_running);
        }

    }

    private void MovePlayer(Ray ray)
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            //Wolf moves up if you click it, we don't want that
            if (hit.transform.name == "Player" || hit.transform.tag == "Wall")
            {
                //is_running = false;
                //return;
            }

            Vector3 localTarget = transform.InverseTransformPoint(hit.point);
            Vector3 newTarget = new Vector3(hit.point.x, transform.position.y, hit.point.z); 

            float angle = Mathf.Atan2(localTarget.x, localTarget.z) * Mathf.Rad2Deg;
            Vector3 eulerAngleVelocity = new Vector3(0, angle, 0);
            Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.fixedDeltaTime * MovementSpeed);

            is_running = true;
            rb.MovePosition(Vector3.MoveTowards(transform.position, newTarget, MovementSpeed * Time.fixedDeltaTime));
            rb.MoveRotation(rb.rotation * deltaRotation);
        }
        else
        {
            is_running = false;
        }
    }

}

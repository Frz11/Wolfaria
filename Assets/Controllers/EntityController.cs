using UnityEngine;
using System.Collections;

public class EntityController : MonoBehaviour
{
    public bool move_to_target = false;
    public Vector3 target;

    protected Quaternion deltaRotation;
    protected Rigidbody rb;
    protected float MovementSpeed;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool MoveToTarget()
    {
        if (System.Math.Abs(transform.position.x - target.x) > 1 || System.Math.Abs(transform.position.z - target.z) > 1)
        {
            this.CalculateRotation();

            rb.MovePosition(Vector3.MoveTowards(transform.position, target, MovementSpeed * Time.deltaTime));
            rb.MoveRotation(rb.rotation * deltaRotation);

            return true;
        }

        return false;
    }

    protected void CalculateRotation()
    {
        Vector3 local = transform.InverseTransformPoint(target);
        float angle = Mathf.Atan2(local.x, local.z) * Mathf.Rad2Deg;
        Vector3 eulerAngleVelocity = new Vector3(0, angle, 0);

        deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.fixedDeltaTime * 5);
    }
}

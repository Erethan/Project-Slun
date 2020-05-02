using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(GroundChecker))]
public class GroundMovement : MonoBehaviour
{
    [Range(0, 15)]
    public float targetSpeed;
    public float impulseForce;

    public Vector3 Velocity { get; private set; }
    private Rigidbody rigid;
    private GroundChecker groundCheck;
    private Vector3 direction;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        groundCheck = GetComponent<GroundChecker>();
        Velocity = new Vector3(0, 0, 0);
        direction = new Vector3(0, 0, 0);
    }

    void Update()
    {
        Vector3 xOzVelocity = Vector3.ProjectOnPlane(Velocity, Vector3.up);
        if (xOzVelocity.sqrMagnitude > 0.1f)
        {
            transform.forward = Vector3.ProjectOnPlane(xOzVelocity, Vector3.up);
        }
    }

    void FixedUpdate()
    {
        Velocity = rigid.velocity;

        //Add movement impulse
        if (Mathf.Abs(direction.x) > float.Epsilon || Mathf.Abs(direction.y) > float.Epsilon)
        {
            Vector3 planeDir = Vector3.ProjectOnPlane(direction, groundCheck.GroundNormal);
            planeDir.Normalize();

            if (Velocity.sqrMagnitude < targetSpeed * targetSpeed)
            {
                rigid.AddForce(planeDir * impulseForce * direction.magnitude, ForceMode.Impulse);
            }
        }
    }


    public void SetDirection(Vector3 direction) => this.direction.Set(direction.x, direction.y, direction.z);
    public void SetDirection(float x, float y, float z) => this.direction.Set(x,y,z);
    public void ResetVelocity() => rigid.velocity = Vector3.zero;
}

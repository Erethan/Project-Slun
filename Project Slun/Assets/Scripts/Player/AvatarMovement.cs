using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(GroundCheck))]
public class AvatarMovement : MonoBehaviour
{
    public FloatingJoystick joystick;
    public float targetSpeed;
    public float impulseForce;

    private Rigidbody rigid;
    private GroundCheck groundCheck;
    public Vector3 Velocity { get; private set; }

    [Header("Drag")]
    public float walkingDrag;
    public float stopedDrag;
    public float airborneDrag;


    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        groundCheck = GetComponent<GroundCheck>();
        Velocity = new Vector3(0, 0, 0);
    }

    void Update()
    {
        if (Velocity.sqrMagnitude > 0.1f)
        {
            transform.forward = Vector3.ProjectOnPlane(Velocity, Vector3.up);
        }
    }

    void FixedUpdate()
    {
        Velocity = rigid.velocity;

        //Add movement impulse
        Vector3 inputAxis = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        if (Mathf.Abs(inputAxis.x) > float.Epsilon || Mathf.Abs(inputAxis.y) > float.Epsilon)
        {
            Vector3 planeDir = Vector3.ProjectOnPlane(inputAxis, groundCheck.GroundNormal);
            planeDir.Normalize();

            if(Velocity.sqrMagnitude < targetSpeed * targetSpeed)
            {
                rigid.AddForce(planeDir * impulseForce * inputAxis.magnitude, ForceMode.Impulse);
            }
        }

        //Update drag
        if (!groundCheck.Grounded)
        {
            rigid.drag = airborneDrag;
        }
        else if(inputAxis.sqrMagnitude > 0.1f)
        {
            rigid.drag = walkingDrag;
        }
        else
        {
            rigid.drag = stopedDrag;
        }
    }
       
}

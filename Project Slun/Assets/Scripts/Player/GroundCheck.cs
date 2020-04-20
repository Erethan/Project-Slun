using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public CapsuleCollider capsuleCollider;
    [Range(0, 0.1f)] public float groundCheckTolerance = 0.01f;
    [Range(0, 1)] public float sphereCastProportion = 1;

    public bool Grounded{get; private set;}
    public Vector3 GroundNormal { get; private set; }


    private void FixedUpdate()
    {
        RaycastHit hitInfo;
        if (Physics.SphereCast(transform.position, capsuleCollider.radius * sphereCastProportion, Vector3.down,
                out hitInfo, (capsuleCollider.height / 2f) + groundCheckTolerance,
                ~0, QueryTriggerInteraction.Ignore))
        {
            GroundNormal = hitInfo.normal;
            Grounded = true;
        }
        else
        {
            GroundNormal = Vector3.up;
            Grounded = false;
        }
    }
    
}

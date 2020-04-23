using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AvatarMovement))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(GroundChecker))]
public class AvatarController : MonoBehaviour
{
    public UnityEvent onDeathAnimationFinish;

    private AvatarMovement movement;
    private GroundChecker groundChecker;
    private Animator animator;

    private void Awake()
    {
        movement = GetComponent<AvatarMovement>();
        groundChecker = GetComponent<GroundChecker>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        animator.SetFloat("Velocity", movement.Velocity.magnitude);
        animator.SetBool("Grounded", groundChecker.Grounded);
    }

    public void DeadAnimationFinishHandler()
    {
        onDeathAnimationFinish.Invoke();
    }

    public void Revive()
    {
        SetDeathState(false);
    }

    public void Kill()
    {
        SetDeathState(true);
    }

    private void SetDeathState(bool isDead)
    {
        animator.SetBool("Dead", isDead);
        movement.enabled = !isDead;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PatrolEnemy : MonoBehaviour
{
    public Vector3 targetOffset;
    public GroundMovement movement;

    private Vector3[] targetWorldPositions;
    private int nextTargetIndex;


    private void Awake()
    {
        targetWorldPositions = new Vector3[1 + 1]; // start + targets position TODO: make targetOffset an array
        targetWorldPositions[0] = transform.position;
        targetWorldPositions[1] = transform.position + targetOffset;
        nextTargetIndex = 1;
    }

    private void Update()
    {
        if( Vector3.Distance(transform.position, targetWorldPositions[nextTargetIndex]) < 0.25f )
        {
            nextTargetIndex++;
            if(nextTargetIndex == targetWorldPositions.Length)
            {
                nextTargetIndex = 0;
            }
            movement.ResetVelocity();
        }
        movement.SetDirection(targetWorldPositions[nextTargetIndex] - transform.position);
    }
    
    public Vector3 GetNextTarget()
    {
        if(targetWorldPositions == null || targetWorldPositions.Length == 0)
        {
            return transform.position;
        }

        return targetWorldPositions [nextTargetIndex];
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(PatrolEnemy))]
public class PatrolEnemyEditor : Editor
{
    private Transform enemyTf;

    protected virtual void OnSceneGUI()
    {
        if (Event.current.type == EventType.Repaint)
        {
            Vector3 targetPos = ((PatrolEnemy)target).GetNextTarget();
            if(enemyTf == null) enemyTf = ((PatrolEnemy)target).transform;

            if (Vector3.Distance(targetPos, enemyTf.position) < 0.1f)
            {
                return;
            }

            Handles.SphereHandleCap(-1, targetPos, Quaternion.identity, 0.1f, EventType.Repaint);
            Handles.DrawAAPolyLine(6, enemyTf.position, targetPos);
        }
    }
}
#endif
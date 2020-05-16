using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PatrolEnemy : MonoBehaviour
{
    public GroundMovement movement;
    //public Vector3 TargetPosition { get { return initialPosition + targetOffset; } }

    [SerializeField]  private Vector3 targetOffset = default;
    [SerializeField] [HideInInspector] private Vector3 initialPosition = default;
    [SerializeField] [HideInInspector] private int nextTargetIndex = 1;

    public Vector3 TargetPosition { get { return initialPosition + targetOffset; } }

    private Vector3 NextTarget
    {
        get
        {
            switch (nextTargetIndex)
            {
                case 0:
                    return initialPosition;
                default:
                    return TargetPosition;
            }
        }
    }

    private void Awake()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        if( Vector3.Distance(transform.position, NextTarget) < 0.25f )
        {
            ChangeTarget();
            movement.ResetVelocity();
        }
        movement.SetDirection(NextTarget - transform.position);
    }

    private void ChangeTarget()
    {
        nextTargetIndex++;
        if (nextTargetIndex > 1)
            nextTargetIndex = 0;
    }
    
}

#if UNITY_EDITOR
[CanEditMultipleObjects]
[CustomEditor(typeof(PatrolEnemy))]
public class PatrolEnemyEditor : Editor
{
    private PatrolEnemy[] selectedPatrolEnemies;
    private SerializedProperty[] initialPositionProps;
    private SerializedProperty[] targetOffsetProps;

    void OnEnable()
    {
        Object[] inspectedObjects = serializedObject.targetObjects;
        selectedPatrolEnemies = new PatrolEnemy[inspectedObjects.Length];
        initialPositionProps = new SerializedProperty[inspectedObjects.Length];
        targetOffsetProps = new SerializedProperty[inspectedObjects.Length];

        for (int i = 0; i < inspectedObjects.Length; i++)
        {
            selectedPatrolEnemies[i] = targets[i] as PatrolEnemy;

            SerializedObject so = new SerializedObject(inspectedObjects[i]);
            initialPositionProps[i] = so.FindProperty("initialPosition");
            targetOffsetProps[i] = so.FindProperty("targetOffset");

        }

    }


    void OnSceneGUI()
    {
        if (!EditorApplication.isPlaying)
        {
            for (int i = 0; i < initialPositionProps.Length; i++)
            {
                initialPositionProps[i].vector3Value = selectedPatrolEnemies[i].transform.position;
            }
        }

        if(!Event.current.control)
        {
            Vector3 centerHandlePos = Vector3.zero;
            Vector3 previousCenterHandlePos;
            for (int i = 0; i < selectedPatrolEnemies.Length; i++)
            {
                centerHandlePos += selectedPatrolEnemies[i].TargetPosition;
                
            }

            centerHandlePos = centerHandlePos / selectedPatrolEnemies.Length;
            previousCenterHandlePos = centerHandlePos;
            centerHandlePos = Handles.PositionHandle(centerHandlePos, Quaternion.identity);

            for (int i = 0; i < initialPositionProps.Length; i++)
            {
                targetOffsetProps[i].vector3Value += centerHandlePos - previousCenterHandlePos;
            }
        }

        for (int i = 0; i < initialPositionProps.Length; i++)
        {

            if (Event.current.control)
            {
                targetOffsetProps[i].vector3Value = Handles.PositionHandle(
                    selectedPatrolEnemies[i].TargetPosition,
                    Quaternion.identity) - initialPositionProps[i].vector3Value;
            }

            if (Event.current.type == EventType.Repaint)
            {
                Handles.SphereHandleCap(-1, selectedPatrolEnemies[i].TargetPosition, Quaternion.identity, 0.25f, EventType.Repaint);
                Handles.DrawAAPolyLine(2, initialPositionProps[i].vector3Value, selectedPatrolEnemies[i].TargetPosition);
            }

            
                targetOffsetProps[i].vector3Value = new Vector3(targetOffsetProps[i].vector3Value.x, 0, targetOffsetProps[i].vector3Value.z);
            targetOffsetProps[i].serializedObject.ApplyModifiedProperties();
        }

    }
}

#endif

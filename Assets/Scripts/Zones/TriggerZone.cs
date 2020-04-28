using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerZone : MonoBehaviour
{
    public GameObjectEvent onEnter, onExit;

    private List<GameObject> gameObjectsInside;
    protected virtual bool InterestedGameObject(GameObject go) => true;

    void Awake()
    {
        gameObjectsInside = new List<GameObject>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (!gameObjectsInside.Contains(other.gameObject))
        {
            gameObjectsInside.Add(other.gameObject);
            onEnter.Invoke(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (gameObjectsInside.Contains(other.gameObject))
        {
            gameObjectsInside.Remove(other.gameObject);
            onExit.Invoke(other.gameObject);
        }
    }

    public virtual GameObject[] ObjectsInside()
    {
        return gameObjectsInside.ToArray();
    }

    

}

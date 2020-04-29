using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public AvatarController avatar;

    private Vector3 startPosition;
    private Quaternion startRotation;

    private void Awake()
    {
        startPosition = avatar.transform.position;
        startRotation = avatar.transform.rotation;
    }


    public void Reset()
    {
        avatar.transform.position = startPosition;
        avatar.transform.rotation = startRotation;
        avatar.Revive();
    }

    public void Quit()
    {

    }

    public void Finish()
    {

    }
}

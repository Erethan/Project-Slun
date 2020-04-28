using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    public void HazardHit(GameObject hitGameObject)
    {
        AvatarController avatar = hitGameObject.GetComponent<AvatarController>();
        if(avatar != null)
        {
            avatar.HazardHit();
        }
    }
}

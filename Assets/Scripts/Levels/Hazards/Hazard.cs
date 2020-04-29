using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    public void HazardHit(GameObject hitGameObject)
    {
        AvatarController avatarHit = hitGameObject.GetComponent<AvatarController>();
        if(avatarHit != null)
        {
            avatarHit.HazardHit();
        }
    }
}

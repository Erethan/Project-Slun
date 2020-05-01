using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    public void HazardHit(AvatarController avatarHit)
    {
        avatarHit.HazardHit();
    }
}

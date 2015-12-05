using UnityEngine;
using System.Collections;

public class RockSlide : PrekazkaBase
{
    public override void Kill()
    {
        GetComponent<BoxCollider>().isTrigger = false;
        GetComponent<BoxCollider>().enabled = false;
    }
}

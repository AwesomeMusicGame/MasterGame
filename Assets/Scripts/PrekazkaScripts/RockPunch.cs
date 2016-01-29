using UnityEngine;
using System.Collections;

public class RockPunch : PrekazkaBase {

    private bool isRekt = false;

    void Update()
    {
        if (isRekt)
        {
            this.transform.RotateAround(Vector3.forward, 2);
        }
        //if (this.transform.rotation.eulerAngles.z < 90) Destroy(this);
    }

    public override void Kill()
    {
        isRekt = true;
    }
}

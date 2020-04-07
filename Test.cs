using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : Steering
{
    // Start is called before the first frame update
    public override Vector3 Force()
    {
        return transform.forward * 10;
    }
}

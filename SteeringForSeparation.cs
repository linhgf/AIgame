using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringForSeparation : Steering
{
    public float comfortDistance = 1f;
    //过近时的惩罚因子
    public float multiplierInsideComfortDistance = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override Vector3 Force()
    {
        Vector3 steeringForce = new Vector3(0, 0, 0);
        foreach (GameObject s in GetComponent<Radar>().neighbors)
        {
            if (s != null && s != this.gameObject)
            {
                Vector3 toNeighbor = transform.position - s.transform.position;
                float length = toNeighbor.magnitude;
                steeringForce += toNeighbor.normalized / length;//成反比
                if (length < comfortDistance)
                    steeringForce *= multiplierInsideComfortDistance;
            }
        }
        return steeringForce;
    }
}

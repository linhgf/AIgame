using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringForCohesion : Steering
{
    private float maxSpeed;
    private Vehicle m_vehicle;
    private Vector3 desiredVelocity;

    // Start is called before the first frame update
    void Start()
    {
        m_vehicle = GetComponent<Vehicle>();
        maxSpeed = m_vehicle.maxSpeed;
    }

    public override Vector3 Force()
    {
        Vector3 steeringForce = new Vector3(0, 0, 0);
        Vector3 centerMass = new Vector3(0, 0, 0);//所有邻居的平均位置
        int neighborsCount = 0;
        foreach (GameObject s in GetComponent<Radar>().neighbors)
        {
            if ((s != null) && (s != this.gameObject))
            {
                neighborsCount++;
                centerMass += s.gameObject.transform.position;
            }
        }
        if (neighborsCount > 0)
        {
            centerMass /= (float)neighborsCount;
            desiredVelocity = (centerMass - transform.position).normalized * maxSpeed;
            steeringForce = desiredVelocity - m_vehicle.velocity;
        }
        return steeringForce;
    }
}

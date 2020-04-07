using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringForArrive : Steering
{
    // Start is called before the first frame update
    public bool isPlaner = true;
    private Vehicle m_vehicle;
    private float maxSpeed;
    public Transform target;
    private Vector3 desiredVelocity;
    private float distance;//记录当前位置
    public float slowDownDistance;

    void Start()
    {
        m_vehicle = GetComponent<Vehicle>();
        maxSpeed = m_vehicle.maxSpeed;
        isPlaner = m_vehicle.isPlanar;
    }

    public override Vector3 Force()
    {
        Vector3 toTraget = target.transform.position - transform.position;
        if (isPlaner)
            toTraget.y = 0;
        distance = toTraget.magnitude;
        if (distance > slowDownDistance)
        {
            desiredVelocity = toTraget.normalized * maxSpeed;
            return (desiredVelocity - m_vehicle.velocity);
        }
        else
        {
            desiredVelocity = toTraget - m_vehicle.velocity;
            return (desiredVelocity - m_vehicle.velocity);
        }
        /*if (distance <= slowDownDistance)
        {
            desiredVelocity = toTraget - m_vehicle.velocity;
            return (desiredVelocity - m_vehicle.velocity);
        }
        else
            return new Vector3(0, 0, 0);*/
    }
}

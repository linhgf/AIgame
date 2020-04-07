using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringForFlee : Steering
{
    public Transform target;
    private Vehicle m_vehicle;
    public float fearDistance = 20f;
    private Vector3 desiredVelocity;
    private float maxSpeed;

    // Start is called before the first frame update
    void Start()
    {
        m_vehicle = GetComponent<Vehicle>();
        maxSpeed = m_vehicle.maxSpeed;
    }

    public override Vector3 Force()
    {
        Vector3 tempPos = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 tempTargetPos = new Vector3(target.position.x, 0, target.position.z);
        if (Vector3.Distance(tempPos, tempTargetPos) > fearDistance)
            return new Vector3(0, 0, 0);
        desiredVelocity = (transform.position - target.transform.position).normalized * maxSpeed;
        return (desiredVelocity - m_vehicle.velocity);
    }

}

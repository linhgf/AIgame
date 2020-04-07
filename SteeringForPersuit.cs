using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringForPersuit : Steering
{
    public Transform target;
    private Vehicle m_vehicle;
    private float maxSpeed;
    private Vector3 desiredVelocity;

    // Start is called before the first frame update
    void Start()
    {
        m_vehicle = GetComponent<Vehicle>();
        maxSpeed = m_vehicle.maxSpeed;
    }

    public override Vector3 Force()
    {
        Vector3 toTarget = target.position - transform.position;
        float relativeDirection = Vector3.Dot(target.forward, transform.forward);//判断两物体的朝向，若小于0则为面对面
        
        if (Vector3.Dot(toTarget, transform.forward) > 0 && relativeDirection < -0.95f)
        {
            //若满足该条件，则正面两物体面对面，且速度方向正确，可直接靠近，无需预测
            desiredVelocity = toTarget.normalized * maxSpeed;
            return (desiredVelocity - m_vehicle.velocity);
        }
        //预测时间和速度成反比，距离成正比
        float predictTime = toTarget.magnitude / (target.GetComponent<Vehicle>().velocity.magnitude + (maxSpeed * 0.6f));
        desiredVelocity = (target.position + target.GetComponent<Vehicle>().velocity * predictTime - transform.position).normalized * maxSpeed;
        return (desiredVelocity - m_vehicle.velocity);
    }
}

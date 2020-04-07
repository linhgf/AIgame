using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringForWander : Steering
{
    //圆相关
    public float wanderRadius;//半径
    public float wanderDistance;//角色前方距离
    public float wanderJitter;//每次随机位移的最大距离
    private Vector3 circleTarget;//圆上的随机一点
    private Vector3 wanderTarget;//圆上点局部坐标转为全局坐标

    //角色相关
    private Vehicle m_vehicle;
    private float maxSpeed;
    private Vector3 desiredVelocity;
    private bool isPlanar;

    // Start is called before the first frame update
    void Start()
    {
        m_vehicle = GetComponent<Vehicle>();
        maxSpeed = m_vehicle.maxSpeed;
        circleTarget = new Vector3(wanderRadius * 0.507f, 0, wanderRadius * 0.507f);//圆上随机选取一点
        isPlanar = m_vehicle.isPlanar;
    }

    public override Vector3 Force()
    {
        //计算随机位移
        Vector3 randomDisplacement = new Vector3((Random.value - 0.5f) * 2 * wanderJitter, (Random.value - 0.5f) * 2 * wanderJitter, (Random.value - 0.5f) * 2 * wanderJitter);
        if (isPlanar)
            randomDisplacement.y = 0;
        circleTarget += randomDisplacement;
        circleTarget = circleTarget.normalized * wanderRadius;
        wanderTarget = transform.position + m_vehicle.velocity.normalized * wanderDistance + circleTarget;
        desiredVelocity = (wanderTarget - transform.position).normalized * maxSpeed;
        return (desiredVelocity - m_vehicle.velocity);
    }

}

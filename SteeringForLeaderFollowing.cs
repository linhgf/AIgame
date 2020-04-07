using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SteeringForArrive))]
public class SteeringForLeaderFollowing : Steering
{
    public Vector3 target;
    private Vector3 desiredVelocity;
    private Vehicle m_vehicle;
    private float maxSpeed;
    private bool isPlanar;

    //领队相关
    public GameObject leader;
    private Vehicle leaderVehicle;
    private Vector3 leaderVelocity;

    //跟随者落后领队的距离
    public float LEADER_BEHIND_DIST = 2.0f;
    private SteeringForArrive arriveScript;
    private Vector3 randomOffset;

    // Start is called before the first frame update
    void Start()
    {
        m_vehicle = GetComponent<Vehicle>();
        maxSpeed = m_vehicle.maxSpeed;
        isPlanar = m_vehicle.isPlanar;
        leaderVehicle = leader.GetComponent<Vehicle>();
        arriveScript = GetComponent<SteeringForArrive>();
        arriveScript.target = new GameObject("arriveTarget").transform;
        arriveScript.target.position = leader.transform.position;
    }

    public override Vector3 Force()
    {
        leaderVelocity = leaderVehicle.velocity;
        target = leader.transform.position + LEADER_BEHIND_DIST * (-leaderVelocity).normalized;
        arriveScript.target.position = target;
        return new Vector3(0, 0, 0);
    }
}

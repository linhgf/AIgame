using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvadeController : MonoBehaviour
{
    //领队相关
    public GameObject leader;
    private Vector3 leaderVelocity;
    private Vehicle leaderVehicle;

    //距离相关
    public float LEADER_BEHIND_DIST;
    public float evadeDistance;
    public Vector3 dist;
    private float sqrEvadeDistance;
    private Vector3 leaderAhead;

    private bool isPlanar;

    //脚本相关
    private SteeringForFlee evadeScript;
    private Vehicle m_vehicle;

    // Start is called before the first frame update
    void Start()
    {
        leaderVehicle = leader.GetComponent<Vehicle>();
        leaderVelocity = leaderVehicle.velocity;
        sqrEvadeDistance = evadeDistance * evadeDistance;
        LEADER_BEHIND_DIST = 2.0f;
        m_vehicle = GetComponent<Vehicle>();
        isPlanar = m_vehicle.isPlanar;
        evadeScript = GetComponent<SteeringForFlee>();
        evadeScript.target = leader.transform;
        evadeScript.fearDistance = evadeDistance;
    }

    // Update is called once per frame
    void Update()
    {
        leaderVelocity = leaderVehicle.velocity;
        leaderAhead = leader.transform.position + leaderVelocity.normalized * LEADER_BEHIND_DIST;
        dist = transform.position - leaderAhead;
        if (isPlanar)
            dist.y = 0;
        if (dist.sqrMagnitude < sqrEvadeDistance)
        {
            evadeScript.enabled = true;
            Debug.DrawLine(transform.position, leader.transform.position);
        }
        else
            evadeScript.enabled = false;
    }
}

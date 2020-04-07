using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringForCollisionAvoidance : Steering
{
    public bool isPlanar;
    private Vector3 desiredVelocity;
    private Vehicle m_vehicle;
    private float maxSpeed;
    private float maxForce;
    public LayerMask obstacle;
    //避免障碍操控力
    public float avoidanceForce;
    //可视距离
    public float MAX_SEE_AHEAD = 2.0f;
    //全部碰撞
    private GameObject[] allColliders;
    // Start is called before the first frame update
    void Start()
    {
        m_vehicle = GetComponent<Vehicle>();
        maxSpeed = m_vehicle.maxSpeed;
        maxForce = m_vehicle.maxForce;
        isPlanar = m_vehicle.isPlanar;
        if (avoidanceForce > maxForce)
            avoidanceForce = maxForce;
        allColliders = GameObject.FindGameObjectsWithTag("Obstacle");
    }

    // Update is called once per frame
    public override Vector3 Force()
    {
        RaycastHit hit;
        Vector3 force = new Vector3(0, 0, 0);
        Vector3 velocity = m_vehicle.velocity;
        Vector3 normalizedVelocity = velocity.normalized;
        Debug.DrawLine(transform.position, transform.position + normalizedVelocity * MAX_SEE_AHEAD * (velocity.magnitude/maxSpeed),Color.red);
        if (Physics.Raycast(transform.position, normalizedVelocity, out hit, MAX_SEE_AHEAD * (velocity.magnitude / maxSpeed),obstacle))
        {
            Vector3 ahead = transform.position + normalizedVelocity * MAX_SEE_AHEAD * (velocity.magnitude / maxSpeed);
            force = ahead - hit.collider.transform.position;
            force *= avoidanceForce;
            if (isPlanar)
                force.y = 0;
        }

        return force;
    }
 }

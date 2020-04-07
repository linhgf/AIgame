using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringForQueueAvoidence : Steering
{
    public bool isPlanar;
    private Vehicle m_vehicle;
    private float maxSpeed;
    private float maxForce;
    public float avoidenceForce;//避开障碍物力的幅度
    private GameObject[] obstacles;//障碍物碰撞体
    public float MAX_SEE_AHEAD;//判断距离
    public LayerMask obstacleLayer;//障碍物的layer

    [Header("Timer")]
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        m_vehicle = GetComponent<Vehicle>();
        maxSpeed = m_vehicle.maxSpeed;
        maxForce = m_vehicle.maxForce;
        obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        isPlanar = m_vehicle.isPlanar;
        if (avoidenceForce > maxForce)
            avoidenceForce = maxForce;
    }

    private void Update()
    {
        Vector3 velocity = m_vehicle.velocity;
        Vector3 normalizedVelocity = velocity.normalized;
        Debug.DrawLine(transform.position, transform.position + normalizedVelocity * MAX_SEE_AHEAD * Mathf.Pow((velocity.magnitude / maxSpeed), 3), Color.red);
    }

    public override Vector3 Force()
    {
        Vector3 velocity = m_vehicle.velocity;
        Vector3 normalizedVelocity = velocity.normalized;
        Vector3 steeringForce = new Vector3(0, 0, 0);
        //用Raycast检测碰撞
        RaycastHit hit;
        if (Physics.Raycast(transform.position, normalizedVelocity, out hit, MAX_SEE_AHEAD * Mathf.Pow((velocity.magnitude / maxSpeed), 3), obstacleLayer))
        {
            Vector3 ahead = transform.position + normalizedVelocity * MAX_SEE_AHEAD;
            steeringForce = ahead - hit.collider.transform.position;
            steeringForce *= avoidenceForce;
            if (isPlanar)
                steeringForce.y = 0;
        }
        return steeringForce;
    }
}

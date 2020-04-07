using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringForQueue : Steering
{
    public float MAX_QUEUE_AHEAD;//用于检测前方是否有其他物体
    public float MAX_QUEUE_RADIUS;//检测范围的半径
    private Collider[] colliders;
    public LayerMask layersChecked;
    private Vehicle m_vehicle;
    private int layerid;
    public float speedDown;//减速幅度
    // Start is called before the first frame update
    void Start()
    {
        m_vehicle = GetComponent<Vehicle>();
    }

    // Update is called once per frame
    public override Vector3 Force()
    {
        Vector3 velocity = m_vehicle.velocity;
        Vector3 normalizedVelocity = velocity.normalized;
        Vector3 ahead = transform.position + normalizedVelocity * MAX_QUEUE_AHEAD;
        colliders = Physics.OverlapSphere(ahead, MAX_QUEUE_RADIUS, layersChecked);
        if (colliders.Length > 0)
        {
            foreach (Collider c in colliders)
            {
                if ((c.gameObject != this.gameObject) && (c.gameObject.GetComponent<Vehicle>().velocity.magnitude < velocity.magnitude))
                {
                    velocity *= speedDown;
                    break;
                }
            }
        }
        return new Vector3(0, 0, 0);
    }
}

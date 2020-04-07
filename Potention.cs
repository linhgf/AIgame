using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potention : Steering
{
    private float maxSpeed;
    private Vehicle m_vehicle;
    private float distance;
    private Vector3 velocity;
    public float MAX_SEE_AHEAD;//检测碰撞的距离
    public LayerMask obstacle;//障碍物layer
    float A, B, n, m, U; //势能函数相关参数

    //力的开关
    public bool swam = true;//群聚
    public bool avoid = true;//避开障碍物
    public bool follow = true;//跟随

    public Transform leader;//设置leader

    // Start is called before the first frame update
    void Start()
    {
        m_vehicle = GetComponent<Vehicle>();
        maxSpeed = m_vehicle.maxSpeed;
    }

    public override Vector3 Force()
    {
        Vector3 steeringForce = new Vector3(0, 0, 0);
        float distance;//物体之间距离
        if (swam)
        {
            //群聚相关
            A = 1000;
            B = 2000;
            n = 1;
            m = 3;
            foreach (GameObject s in GetComponent<Radar>().neighbors)
            {
                if (s != null && s != gameObject)
                {
                    distance = Vector3.Distance(transform.position, s.transform.position);
                    U = (-A) / Mathf.Pow(distance, n) + B / Mathf.Pow(distance, m);
                    steeringForce += (transform.position - s.transform.position).normalized * U;
                }
            }
        }

        if (follow)
        {
            //跟随leader相关
            float toTarget = Vector3.Distance(leader.position, transform.position);//leader和跟随者的距离
            //跟随参数
            A = 10000;
            B = 50;
            n = 3;
            m = 1;
            U = (-A) / Mathf.Pow(toTarget, n) + B / Mathf.Pow(toTarget, m);
            steeringForce += (transform.position - leader.position).normalized * U;//注意力的方向
        }

        if(avoid)
        {
            //检测碰撞相关
            RaycastHit hit;
            velocity = m_vehicle.velocity;
            Vector3 normalizedVelocity = velocity.normalized;
            if (Physics.Raycast(transform.position, normalizedVelocity, out hit, MAX_SEE_AHEAD, obstacle))//检测碰撞
            {
                //碰撞参数
                A = 0;
                B = 2000;
                n = 1;
                m = 2;
                distance = Vector3.Distance(transform.position, hit.point) / MAX_SEE_AHEAD;//计算距离,除以MAX_SEE_AHEAD可以让距离保持在0-1，解决距离过远导致力不明显的问题
                U = (-A) / Mathf.Pow(distance, n) + B / Mathf.Pow(distance, m);
                steeringForce += (transform.position - hit.point).normalized * U;
            }
        }
        return steeringForce;
    }
}

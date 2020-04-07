using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test2 : Steering
{
    private float maxSpeed;
    private Vehicle m_vehicle;
    private float distance;
    public float MAX_SEE_AHEAD = 5f;
    public LayerMask obstacle;
    // Start is called before the first frame update
    void Start()
    {
        m_vehicle = GetComponent<Vehicle>();
        maxSpeed = m_vehicle.maxSpeed;
    }

    private void Update()
    {
        Vector3 velocity = m_vehicle.velocity;
        Vector3 normalizedVelocity = velocity.normalized;
        Debug.DrawLine(transform.position, transform.position + normalizedVelocity * MAX_SEE_AHEAD, Color.red);
    }
    public override Vector3 Force()
    {
        Vector3 steeringForce = new Vector3(0, 0, 0);
        Vector3 velocity = m_vehicle.velocity;
        Vector3 normalizedVelocity = velocity.normalized;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, normalizedVelocity, out hit, MAX_SEE_AHEAD,obstacle))
        {
            //hit.point代表的是与射线接触到的点的坐标，而不是接触到的物体的坐标
            distance = (transform.position - hit.point).magnitude;
            float A, B, n, m, U; //势能函数相关参数
            A = 0;
            B = 2000f;
            n = 1;
            m = 2;
            //开始反弹的时间应该是>=MAX_SEE_AHEAD可视距离的时候，因此distance除以MAX_SEE_AHEAD可以把距离缩小到0-1（这样可以更好地达成跟距离成反比并且越近力变化的幅度越大）
            U = (-A / Mathf.Pow(distance, n)) +( B / Mathf.Pow(distance/MAX_SEE_AHEAD,m));
            //返回力
            steeringForce = (transform.position - hit.point).normalized * U;
            //Debug.Log(steeringForce);
        }
        return steeringForce;
    }

}

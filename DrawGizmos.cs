using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGizmos : MonoBehaviour
{
    //触发逃避的距离
    public float evadeDistance;
    //在leader前画一个范围
    private Vector3 center;
    private Vehicle vehicleScirpt;
    public float LEADER_BEHIND_DIST;
    // Start is called before the first frame update
    void Start()
    {
        vehicleScirpt = GetComponent<Vehicle>();
        LEADER_BEHIND_DIST = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        center = transform.position + vehicleScirpt.velocity.normalized * LEADER_BEHIND_DIST;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(center, evadeDistance);
    }
}

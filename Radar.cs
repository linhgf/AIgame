using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    private Collider[] colliders;
    public LayerMask layer;
    private float timer;
    public List<GameObject> neighbors;//邻居
    public float checkInterval = 0.3f;//间隔
    public float checkRadius = 10f;//检测半径
    public float checkAngle = 360;//可视角度
    private float tempAngle;

    // Start is called before the first frame update
    void Start()
    {
        neighbors = new List<GameObject>();
        timer = 0f;
        tempAngle = checkAngle / 2f;//转为一半，因为在unity中角度为0-180；
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.time;
        if (timer > checkInterval)
        {
            //Debug.Log(neighbors.ToArray().Length);
            neighbors.Clear();//清除邻居
            colliders = Physics.OverlapSphere(transform.position, checkRadius, layer);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].GetComponent<Vehicle>())
                {
                    float angle = Mathf.Acos(Vector3.Dot((colliders[i].gameObject.transform.position - transform.position).normalized, transform.forward.normalized)) * Mathf.Rad2Deg;//计算角度
                    if (angle < tempAngle)
                    {
                        neighbors.Add(colliders[i].gameObject);
                    }
                    
                }
                    
            }
            //Debug.Log(neighbors.ToArray().Length);
            timer = 0;
        }
    }
}
